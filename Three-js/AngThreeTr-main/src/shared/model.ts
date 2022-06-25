import { Vector3 } from "three"
import * as Triangulation from "src/shared/Triangulation"

export class P {
    x: number;
    y: number;
    z: number;
    vec3: Vector3;
    angle: number;
    constructor(x: number, y: number, z: number) {
      this.x = x;
      this.y = y;
      this.z = z;
      this.vec3 = new Vector3(x, y, z);
      this.angle = 0;
    }
    Normalize() {
      this.vec3 = this.vec3.normalize();
      this.x = this.vec3.x;
      this.y = this.vec3.y;
      this.z = this.vec3.z;
    }
    static PointsToVec3(points: P[]): Vector3[] {
      let vec: Vector3[] = [];
      points.forEach(i => {
        vec.push(i.vec3);
      })
      return vec;
    }
    static GetLength(p1: P, p2: P): number {
      let length = Math.sqrt(Math.pow(p1.x - p2.x, 2) +
        Math.pow(p1.y - p2.y, 2) + Math.pow(p1.z - p2.z, 2))
      return length;
    }
  }
  
  
  export class E {
    points: P[] = [];
    length!: number;
    static CreateEdgeFromPoints(points: P[]): E[] {
      let edges: E[] = [];
      for (let i = 0; i < points.length - 1; i++) {
        let pt1 = points[i];
        let pt2 = points[i + 1];
        edges.push(this.CreateEdgeFrom2Point(pt1, pt2));
      }
      return edges;
    }
    static CreateEdgeFrom2Point(point1: P, point2: P): E {
      let edge = new E();
      edge.points.push(point1);
      edge.points.push(point2);
      edge.length = edge.points.length;
      return edge
    }
    GetLength(): number {
      let length = 0;
      for (let i = 0; i < this.points.length - 1; i++) {
        let pt1 = this.points[i];
        let pt2 = this.points[i + 1];
        length += Math.sqrt(
          Math.pow(pt2.x - pt1.x, 2) +
          Math.pow(pt2.y - pt1.y, 2) +
          Math.pow(pt2.z - pt1.z, 2))
      }
      return length;
    }
  }
  
  
  export class F {
    points: P[] = [];
    edges: E[] = [];
    normal: P = new P(1, 1, -1);
  
    static CreateFaceFromPoints(points: P[]): F {
      let face = new F();
      face.points = points.slice(0);
      face.edges = E.CreateEdgeFromPoints(face.points);
      let lastEdge = E.CreateEdgeFrom2Point(face.points[0], face.points[face.points.length - 1]);
      face.edges.push(lastEdge);
      face.normal = Triangulation.GetNormal(face.points[0], face.points[1], face.points[2]);
      face.normal.Normalize();
      return face;
    }
    static RemovePointFromMesh(mesh: F, point: P): F {
      let m = new F();
      m.edges = mesh.edges.slice(0);
      m.points = mesh.points.slice(0);
      for (let i = 0; i < m.points.length; i++) {
        if (m.points[i] == point) {
          for (let j = i; j < m.points.length - 1; j++) {
            m.points[j] = m.points[j + 1]
          }
          m.points.pop();
        }
      }
      for (let i = 0; i < m.edges.length; i++) {
        m.edges[i].points.forEach(p => {
          if (p == point)
            m.edges.splice(i, 1);
        })
      }
      return m;
    }
  }
  
  
  export class B {
    points: P[] = [];
    edges: E[] = [];
    faces: F[] = [];
    mesh: F[] = []
    static CreateSolid(faces: F[]): B {
      let body = new B();
      body.faces = faces;
      body.mesh = body.faces.slice(0);
  
      for (let item of faces) {
        item.edges.forEach(i => {
          body.edges.push(i);
        })
        item.points.forEach(i => {
          body.points.push(i);
        })
      }
      return body;
    }
  }