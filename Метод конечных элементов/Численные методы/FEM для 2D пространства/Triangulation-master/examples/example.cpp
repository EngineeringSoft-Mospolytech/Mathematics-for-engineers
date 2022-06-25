#include "triangulation.h"

#include <iostream>
#include <sstream>
#include <fstream>
#include <cstdlib>
#include <vector>

using std::cout;
using std::endl;
using std::string;
using std::vector;
using namespace Eigen;

void saveMesh(const string &file, const vector<Eigen::Vector3d> &verts, const vector<Eigen::Vector3i> &faces)
{
    string outDir = file;
    std::ofstream out = std::ofstream(outDir);
    for (int i = 0; i < verts.size(); i++)
    {
        out << "v ";
        out << verts[i][0] << " " << verts[i][1] << " " << verts[i][2];
        out << endl;
    }
    for (int i = 0; i < faces.size(); i++)
    {
        out << "f ";
        out << faces[i][0] + 1 << " " << faces[i][1] + 1 << " " << faces[i][2] + 1;
        out << endl;
    }
    out.close();
}

void triangulateSquare()
{
    Polygon poly;
    poly.addPt(Vector3d(0.0, 0.0, 0.0));
    poly.addPt(Vector3d(5.0, 0.0, 0.0));
    poly.addPt(Vector3d(5.0, 5.0, 0.0));
    poly.addPt(Vector3d(0.0, 5.0, 0.0));

    vector<Eigen::Vector3d> verts;
    vector<Eigen::Vector3i> faces;

    Triangulator tri;
    tri.set_quiet(true);
    tri.set_maximun_area(0.2);
    tri.set_minimun_angle(30);
    tri.triangulatePolygon(poly, verts, faces);
    saveMesh("tripoly.obj", verts, faces);
}

void triangulateSquareWithHoles()
{
    Polygon poly;
    poly.addPt(Vector3d(0.0, 0.0, 0.0));
    poly.addPt(Vector3d(5.0, 0.0, 0.0));
    poly.addPt(Vector3d(5.0, 5.0, 0.0));
    poly.addPt(Vector3d(0.0, 5.0, 0.0));

    Hole hole1;
    hole1.addPt(Vector3d(1.0, 1.0, 0.0));
    hole1.addPt(Vector3d(1.0, 2.0, 0.0));
    hole1.addPt(Vector3d(2.0, 2.0, 0.0));
    hole1.addPt(Vector3d(2.0, 1.0, 0.0));
    Hole hole2;
    hole2.addPt(Vector3d(3.0, 2.5, 0.0));
    hole2.addPt(Vector3d(3.0, 4.0, 0.0));
    hole2.addPt(Vector3d(4.0, 4.0, 0.0));
    hole2.addPt(Vector3d(4.0, 3, 0.0));

    //vector<Hole> holes{ hole1 };
    vector<Hole> holes{hole1, hole2};

    vector<Eigen::Vector3d> verts;
    vector<Eigen::Vector3i> faces;
    Triangulator tri;
    tri.set_quiet(true);
    tri.set_maximun_area(0.2);
    tri.set_minimun_angle(30);
    tri.triangulatePolygonWithHoles(poly, holes, verts, faces);
    saveMesh("tripolyholes.obj", verts, faces);
}

int main()
{
    triangulateSquare();

    triangulateSquareWithHoles();

    return 0;
}