import { P, E, F, B } from "src/shared/model"

export function CreateObject(x0: number, y0: number, z0: number, w: number, h: number, n: number) {
    let faces: F[] = [];
    let angle = 2 * Math.PI / n;
    let points1: P[] = [];
    let points2: P[] = [];
    for (let i = 0; i < n; i++) {
        let x = Math.cos(angle * i) * w + x0;
        let z = Math.sin(angle * i) * w + z0;
        x = Number.parseFloat(x.toFixed(5))
        z = Number.parseFloat(z.toFixed(5))
        let pt1 = new P(x, y0, z);
        let pt2 = new P(x, y0 + h, z)
        points1.push(pt1);
        points2.push(pt2);
    }
    let face1 = F.CreateFaceFromPoints(points1);
    let face2 = F.CreateFaceFromPoints(points2);
    faces.push(face1, face2);
    for (let i = 0; i < n; i++) {
        let pts: P[] = [];
        pts = pts.concat(face1.edges[i].points, face2.edges[i].points.reverse());
        let fc = F.CreateFaceFromPoints(pts);
        faces.push(fc)
    }
    let body = B.CreateSolid(faces)
    return body;
}