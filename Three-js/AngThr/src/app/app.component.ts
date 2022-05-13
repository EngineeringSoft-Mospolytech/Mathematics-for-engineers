import { Component } from '@angular/core';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { Color, Mesh, Side, Vector2, Vector3 } from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';
import { B, E, F, P } from 'src/shared/model';
import * as Triangulation from 'src/shared/Triangulation'
import * as Geometry from 'src/shared/geometry'


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngThr';
}



// Создание сцены
const scene = new THREE.Scene();
scene.add(new THREE.AxesHelper(4))
const camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 1, 500);
camera.position.set(0, 0, 10);
camera.lookAt(0, 0, 0);
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth * 0.9, window.innerHeight * 0.9);
document.body.appendChild(renderer.domElement);
var controls = new OrbitControls(camera, renderer.domElement);
renderer.setClearColor(0x222222, 1);


controls.update();

function animation() {
  requestAnimationFrame(animation);
  controls.update();
  renderer.render(scene, camera);
}
animation()
//

// ГРАФИЧЕСКИЕ МЕТОДЫ
// Триангуляция
let body1 = Geometry.CreateObject(1, 0, 0, 1, 1, 3);
Triangulation.TriangulateBody(body1, 0.5);
console.log(body1);

let body2 = Geometry.CreateObject(0, 0, 0, 1, 2, 50);
Triangulation.TriangulateBody(body2, 0.5);
console.log(body2);

//Отображение тела 1
body1.mesh.forEach(mesh => {
  let c1 = Math.random();
  let c2 = 0x00FF00;
  let c3 = Math.random();
  let arr = P.PointsToVec3(mesh.points);
  let geom = new THREE.BufferGeometry().setFromPoints(arr);
  let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
  let faceReset = new THREE.Mesh(geom, mat);
  scene.add(faceReset);
})
//

//Отображение тела 2
body2.mesh.forEach(mesh => {
  let c1 = 0xff0000;
  let c2 = Math.random();
  let c3 = Math.random();
  let arr = P.PointsToVec3(mesh.points);
  let geom = new THREE.BufferGeometry().setFromPoints(arr);
  let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
  let faceReset = new THREE.Mesh(geom, mat);
  scene.add(faceReset);
})
//