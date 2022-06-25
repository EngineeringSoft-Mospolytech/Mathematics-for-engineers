import { P, E, F, B } from "src/shared/model"

/* Получение нормали по 3 точкам */
export function GetNormal(p1: P, p2: P, p3: P) {
    if (p1 != undefined && p2 != undefined && p3 != undefined) {
        let v1 = new P(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z)
        let v2 = new P(p3.x - p1.x, p3.y - p1.y, p3.z - p1.z)
        let normal = new P(
            -(v1.y * v2.z - v1.z * v2.y),
            -(v1.z * v2.x - v1.x * v1.z),
            -(v1.x * v2.y - v1.y * v2.x));
        return normal;
    }
    return new P(0, 0, 0);
}

/* Скалярное умножение векторов */
function Dot(x: P, y: P) {
    return x.x * y.x + x.y * y.y + x.z * y.z;
}

/* перекрестное произведение/перпендикуляр к двум векторам */
function Cross(x: P, y: P) {
    return new P
        (x.y * y.z - x.z * y.y,
            x.z * y.x - x.x * y.z,
            x.x * y.y - x.y * y.x);
}

/* Средняя точка между двумя векторами */
function VectorBetweenPoints(p1: P, p2: P) {
    return new P(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
}

function MidPoint(p1: P, p2: P) {
    return new P((p2.x + p1.x) / 2, (p2.y + p1.y) / 2, (p2.z + p1.z) / 2);
}

/* Угол от (cp,dot(v1,v3))*/
function GetAngle(p1: P, p2: P, p3: P, normal: P) {
    let v1 = VectorBetweenPoints(p2, p1);
    let v3 = VectorBetweenPoints(p2, p3);
    v1.Normalize();
    v3.Normalize();
    let cp = Cross(v1, v3)
    let det = cp.x * normal.x + cp.y * normal.y + cp.z * normal.z;
    let angle = Math.atan2(det, Dot(v1, v3));
    return angle;
}

export function triangulateMesh(mesh: F) {
    let m = new F();
    let n = mesh.points.length;
    m.edges = mesh.edges.slice(0);
    m.points = mesh.points.slice(0)
    let i = 0;
    let w = 0;
    let newFaces = [];
    while (mesh.points.length > 3) {
        let A = mesh.points[i % mesh.points.length]
        let B = mesh.points[(i + 1) % mesh.points.length]
        let C = mesh.points[(i + 2) % mesh.points.length]
        let angle = GetAngle(A, B, C, mesh.normal)
        if (angle > 0) {
            let nPointsOutside = 0;
            for (let j = 0; j < mesh.points.length; j++) {
                if (j < i || j > i + 2) {
                    let P = mesh.points[j];
                    let AB = VectorBetweenPoints(A, B);
                    let BC = VectorBetweenPoints(B, C);
                    let CA = VectorBetweenPoints(C, A);
                    let AP = VectorBetweenPoints(A, P);
                    let BP = VectorBetweenPoints(B, P);
                    let CP = VectorBetweenPoints(C, P);

                    let N1 = Cross(AB, mesh.normal);
                    let N2 = Cross(BC, mesh.normal);
                    let N3 = Cross(CA, mesh.normal);

                    let S1 = Dot(AP, N1);
                    let S2 = Dot(BP, N1);
                    let S3 = Dot(BP, N2);
                    let S4 = Dot(CP, N2);
                    let S5 = Dot(CP, N3);
                    let S6 = Dot(AP, N3);

                    if ((S1 > 0 && S2 > 0 && S3 > 0 && S4 > 0 && S5 > 0 && S6 > 0) || (S1 < 0 && S2 < 0 && S3 < 0 && S4 < 0 && S5 < 0 && S6 < 0)) {
                        i++;
                        i = i % mesh.points.length;
                        break;
                    }
                    else {
                        nPointsOutside++;
                        if (nPointsOutside == mesh.points.length - 3) {
                            let pts = [];
                            pts.push(A);
                            pts.push(B);
                            pts.push(C);
                            mesh = F.RemovePointFromMesh(mesh, B);
                            let newFace = F.CreateFaceFromPoints(pts);
                            newFaces.push(newFace);
                            newFace.normal = mesh.normal;
                            if (mesh.points.length == 3) {
                                break;
                            }
                            i++;
                            i = i % mesh.points.length;
                        }
                    }
                }
            }
        }
        i++;
        i = i % mesh.points.length;
        w++;
        if (w > n)
            break;
    }
    let A = mesh.points[0];
    let B = mesh.points[1];
    let C = mesh.points[2];
    let pts = [];
    pts.push(A);
    pts.push(B);
    pts.push(C);
    let face2 = F.CreateFaceFromPoints(pts);
    newFaces.push(face2);
    return newFaces;
}

export function TriangulateBody(body: B, meshSize: number) {
    let nOfPolygons = 0;
    for (let i = 0; i < 10; i++) {
        let newMesh: F[] = [];
        body.mesh.forEach(mesh => {
            let tri: F[] = [];
            if (mesh.points.length != 3) {
                let faces: F[] = [mesh]
                for (let j = 0; j < 10; j++) {
                    let nf: F[] = []
                    faces.forEach(face => {
                        PreCreateMesh(face, meshSize).forEach(fc => nf.push(fc))
                    })
                    faces = nf
                }
                faces.forEach(face => {
                    triangulateMesh(face).forEach(face => tri.push(face))
                })
            }
            else tri = [mesh];
            tri.forEach(face => {
                let faces: F[] = []
                faces = TriangleAccuracy(face, meshSize)
                faces.forEach(face => { newMesh.push(face) })

            });
        })
        if (nOfPolygons == newMesh.length)
            break;
        nOfPolygons = newMesh.length;
        body.mesh = newMesh;
        console.log(newMesh)
    }
}

function PreCreateMesh(mesh: F, acc: number) {
    let newMeshes = [mesh];
    let midPts = [];
    let j = [];
    if (mesh.points.length == 4) {
        for (let i = 0; i < mesh.edges.length; i++) {
            if (mesh.edges[i].GetLength() >= acc && mesh.edges[i].GetLength() >= mesh.edges[(i + 1) % mesh.edges.length].GetLength()) {
                j.push(i);
                midPts.push(MidPoint(mesh.edges[i].points[0], mesh.edges[i].points[1]));
            }
        }
        let newPts = []
        for (let i = 0; i < 4; i++) {
            newPts.push(mesh.points[i]);
            for (let k = 0; k < j.length; k++) {
                if (j[k] == i) {
                    newPts.push(midPts[k]);
                }
            }
        }
        if (newPts.length == 6) {
            if (j[0] % 2 == 1) {
                newMeshes = [];
                newMeshes.push(F.CreateFaceFromPoints([newPts[0], newPts[1], newPts[2], newPts[5]]))
                newMeshes.push(F.CreateFaceFromPoints([newPts[5], newPts[2], newPts[3], newPts[4]]))
            }
            else {
                newMeshes = [];
                newMeshes.push(F.CreateFaceFromPoints([newPts[0], newPts[1], newPts[4], newPts[5]]))
                newMeshes.push(F.CreateFaceFromPoints([newPts[1], newPts[2], newPts[3], newPts[4]]))
            }
        }
    }
    return newMeshes;
}

function TriangleAccuracy(mesh: F, acc: number) {
    let newMeshes = [mesh];
    let j = [];
    let midPts = [];
    if (mesh.edges.length == 3) {
        for (let i = 0; i < mesh.edges.length; i++) {
            if (mesh.edges[i].GetLength() >= acc) {
                j.push(i);
                midPts.push(MidPoint(mesh.edges[i].points[1], mesh.edges[i].points[0]));
            }
        }
        let newPts = []
        for (let i = 0; i < 3; i++) {
            newPts.push(mesh.points[i])
            for (let k = 0; k < j.length; k++) {
                if (j[k] == i) {
                    newPts.push(midPts[k]);
                }
            }
        }
        if (newPts.length == 6) {
            newMeshes = [];
            newMeshes.push(F.CreateFaceFromPoints([newPts[0], newPts[1], newPts[5]]))
            newMeshes.push(F.CreateFaceFromPoints([newPts[1], newPts[2], newPts[3]]))
            newMeshes.push(F.CreateFaceFromPoints([newPts[3], newPts[4], newPts[5]]))
            newMeshes.push(F.CreateFaceFromPoints([newPts[1], newPts[3], newPts[5]]))
        }
    }

    return newMeshes
}