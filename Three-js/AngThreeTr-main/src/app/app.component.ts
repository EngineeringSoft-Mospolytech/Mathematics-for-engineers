import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { BufferGeometry, Color, Mesh, Side, Vector2, Vector3 } from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';
import { B, E, F, P } from 'src/shared/model';
import { FormsModule } from '@angular/forms';
import * as Triangulation from 'src/shared/Triangulation'
import * as Geometry from 'src/shared/geometry'
// import { ThisReceiver } from '@angular/compiler';
import { models } from '../app/shared/models';
import { HttpDBService } from 'src/app/shared/services/http-db.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { timeStamp } from 'console';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  constructor(private httpDBService: HttpDBService) { }
  private fb: FormBuilder = new FormBuilder;


  @ViewChild(`canvas`) canvasRef!: ElementRef;

  private get canvas(): HTMLCanvasElement {
    return this.canvasRef.nativeElement;
  }


  modeles: models[] = [];
  patchForm!: FormGroup;
  isModelsCreate:boolean = false;

  async getData() {
    // const controls = {
    //   mX: [null, [Validators.required, Validators.maxLength(100)]],
    //   mY: [null, [Validators.required,Validators.maxLength(100)]],
    //   mZ: [null, [Validators.required,Validators.maxLength(100)]],
    //   mN: [null, [Validators.required,Validators.maxLength(100)]],
    //   mL: [null, [Validators.required,Validators.maxLength(100)]],
    //   mH: [null, [Validators.required,Validators.maxLength(100)]]
    // };


    // this.patchForm = this.fb.group(controls);

    // this.patchForm.reset();
    try {
      this.modeles = await this.httpDBService.getmodels();
    } catch (err) {
      console.error(err);
    }
    if(this.isModelsCreate == false){
      for (let i = 0; i < this.modeles.length; i++) {
        let body = Geometry.CreateObject(this.modeles[i].mX, this.modeles[i].mY, this.modeles[i].mZ, this.modeles[i].mL, this.modeles[i].mH, this.modeles[i].mN);
        Triangulation.TriangulateBody(body, 0.5);
  
        body.mesh.forEach(mesh => {
          let c1 = Math.random();
          let c2 = Math.random();
          let c3 = Math.random();
          let arr = P.PointsToVec3(mesh.points);
          let geom = new THREE.BufferGeometry().setFromPoints(arr);
          let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
          let faceReset = new THREE.Mesh(geom, mat);
          faceReset.name = `${this.modeles[i].id}`
          this.scene.add(faceReset);
          
        })
  
      }
      this.isModelsCreate = true;
    }
    else{
      this.createBodyForID(this.modeles.length-1);
    }
  }



  title = 'AngThr';
  X: string = "";
  Y: string = "";
  Z: string = "";
  N: string = "";
  L: string = "";
  H: string = "";
  private camera!: THREE.PerspectiveCamera;

  private controls!: OrbitControls;

  private ambientLight!: THREE.AmbientLight;


  private directionalLight!: THREE.DirectionalLight;

  private loaderGLTF = new GLTFLoader();

  private renderer!: THREE.WebGLRenderer;

  private scene!: THREE.Scene;

  private annotation!: HTMLDivElement;

  private CreateScene() {
    this.scene = new THREE.Scene();
    this.scene.add(new THREE.AxesHelper(4));
    this.camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 1, 500);
    this.camera.position.set(7, 5, 7);
    this.camera.lookAt(0, 0, 0);
  }

  private startRenderingLoop() {
    this.renderer = new THREE.WebGLRenderer({ canvas: this.canvas });
    this.renderer.setSize(this.canvas.clientWidth, this.canvas.clientHeight);
    // document.body.appendChild(this.renderer.domElement);

    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableZoom = true;
    this.renderer.setClearColor(0x222222, 1);
    this.controls.update();

    // let body1 = Geometry.CreateObject(1, 0, 0, 1, 1, 3);
    // Triangulation.TriangulateBody(body1, 0.5);
    // let body2 = Geometry.CreateObject(0, 0, 0, 1, 2, 50);
    // Triangulation.TriangulateBody(body2, 0.5);

    // body1.mesh.forEach(mesh => {
    //   let c1 = Math.random();
    //   let c2 = 0x00FF00;
    //   let c3 = Math.random();
    //   let arr = P.PointsToVec3(mesh.points);
    //   let geom = new THREE.BufferGeometry().setFromPoints(arr);
    //   let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
    //   let faceReset = new THREE.Mesh(geom, mat);
    //   this.scene.add(faceReset);
    // })
    // body2.mesh.forEach(mesh => {
    //   let c1 = 0xff0000;
    //   let c2 = Math.random();
    //   let c3 = Math.random();
    //   let arr = P.PointsToVec3(mesh.points);
    //   let geom = new THREE.BufferGeometry().setFromPoints(arr);
    //   let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
    //   let faceReset = new THREE.Mesh(geom, mat);
    //   this.scene.add(faceReset);
    // })
    let component: AppComponent = this;
    (function render() {
      requestAnimationFrame(render);
      component.renderer.render(component.scene, component.camera);
    }());
    this.renderer.render(this.scene, this.camera);
  }

  // private animation() {
  //   requestAnimationFrame(this.animation);
  //   this.controls.update();
  //   this.renderer.render(this.scene, this.camera);
  // }

  ngAfterViewInit(): void {
    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.
    this.CreateScene();
    this.startRenderingLoop();
    this.getData();
    this.createBodyFor();

  }

 delete3DOBJ(objName:string){
    var selectedObject = this.scene.getObjectByName(objName);
    if(typeof selectedObject != "undefined"){
      this.scene.remove( selectedObject );
      console.log(selectedObject);
    }
}

  async createBodyFor() {
    return new Promise((resolve, reject) => {
      console.log(this.modeles.length);
      for (let i = 0; i < this.modeles.length; i++) {
        let body = Geometry.CreateObject(this.modeles[i].mX, this.modeles[i].mY, this.modeles[i].mZ, this.modeles[i].mL, this.modeles[i].mH, this.modeles[i].mN);
        Triangulation.TriangulateBody(body, 0.5);

        body.mesh.forEach(mesh => {
          let c1 = Math.random();
          let c2 = Math.random();
          let c3 = Math.random();
          let arr = P.PointsToVec3(mesh.points);
          let geom = new THREE.BufferGeometry().setFromPoints(arr);
          let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
          let faceReset = new THREE.Mesh(geom, mat);
          this.scene.add(faceReset);
        })

      }
    });
  }
  createBodyForID(i:number){
      let body = Geometry.CreateObject(this.modeles[i].mX, this.modeles[i].mY, this.modeles[i].mZ, this.modeles[i].mL, this.modeles[i].mH, this.modeles[i].mN);
      Triangulation.TriangulateBody(body, 0.5);

      body.mesh.forEach(mesh => {
        let c1 = Math.random();
        let c2 = Math.random();
        let c3 = Math.random();
        let arr = P.PointsToVec3(mesh.points);
        let geom = new THREE.BufferGeometry().setFromPoints(arr);
        let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
        let faceReset = new THREE.Mesh(geom, mat);
        faceReset.name = `${this.modeles[i].id}`
        this.scene.add(faceReset);
      })

  }
  createBodyForClick() {
    console.log(Number(this.Y));
    let x0: number = Number(this.X);
    let y0: number = Number(this.Y);
    let z0: number = Number(this.Z);
    let l0: number = Number(this.L);
    let h0: number = Number(this.H);
    let n0: number = Number(this.N);
    // let body = Geometry.CreateObject(Number(this.X), Number(this.Y), Number(this.Z), Number(this.L), Number(this.H), Number(this.N));
    let body = Geometry.CreateObject(x0, y0, z0, l0, h0, n0);
    Triangulation.TriangulateBody(body, 0.5);

    body.mesh.forEach(mesh => {
      let c1 = Math.random();
      let c2 = Math.random();
      let c3 = Math.random();
      let arr = P.PointsToVec3(mesh.points);
      let geom = new THREE.BufferGeometry().setFromPoints(arr);
      let mat = new THREE.MeshBasicMaterial({ color: new Color(c1, c2, c3), side: THREE.DoubleSide });
      let faceReset = new THREE.Mesh(geom, mat);
      this.scene.add(faceReset);
    })

  }
  async getModels() {
    try {
      this.modeles = await this.httpDBService.getmodels();
    } catch (err) {
      console.error(err);
    }
  }
  async onDeleteModel(index?: number) {
    if (typeof index != "undefined") this.delete3DOBJ(String(index));
    console.log(index);
    if (typeof index == "undefined") { console.log(index); }
    else {
      console.log(index);
      try {
        await this.httpDBService.deletemodels(index);
      } catch (err) {
        console.error(err);
      }
    }
    // this.sortModelsByID();
    this.getModels();
    this.getData();
  }

  // sortModelsByID() {
  //   this.modeles.sort((a, b) => {
  //     return a.id - b.id;
  //   })
  // }

  async addModels() {
    var model: models = {
      mX: Number(this.X),
      mY: Number(this.Y),
      mZ: Number(this.Z),
      mL: Number(this.L),
      mH: Number(this.H),
      mN: Number(this.N)

    }
    console.log(model);
    try {
      await this.httpDBService.postmodels(model);

    } catch (err) {
      console.error(err);
    }
    this.getData();
  }

}
