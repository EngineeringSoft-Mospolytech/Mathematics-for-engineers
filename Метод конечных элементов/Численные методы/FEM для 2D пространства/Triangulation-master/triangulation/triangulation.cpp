#include "triangulation.h"

#include <iostream>

#define REAL double
#define VOID int

extern "C"
{
#include <triangle/triangle.h>
}

using std::cout;
using std::endl;
using std::string;
using std::vector;

void initTriangulateio(triangulateio &io)
{
    io.pointlist = NULL;
    io.pointattributelist = NULL;
    io.pointmarkerlist = NULL;
    io.trianglelist = NULL;
    io.triangleattributelist = NULL;
    io.trianglearealist = NULL;
    io.neighborlist = NULL;
    io.segmentlist = NULL;
    io.segmentmarkerlist = NULL;
    io.holelist = NULL;
    io.regionlist = NULL;
    io.edgelist = NULL;
    io.edgemarkerlist = NULL;
    io.normlist = NULL;
    io.numberofpoints = 0;
    io.numberofpointattributes = 0;
    io.numberoftriangles = 0;
    io.numberofcorners = 0;
    io.numberoftriangleattributes = 0;
    io.numberofsegments = 0;
    io.numberofholes = 0;
    io.numberofregions = 0;
    io.numberofedges = 0;
}

void freeTriangualteio(triangulateio &io)
{
    if (io.pointlist)
    {
        free(io.pointlist);
        io.pointlist = NULL;
    }
    if (io.pointattributelist)
    {
        free(io.pointattributelist);
        io.pointattributelist = NULL;
    }
    if (io.pointmarkerlist)
    {
        free(io.pointmarkerlist);
        io.pointmarkerlist = NULL;
    }
    if (io.segmentlist)
    {
        free(io.segmentlist);
        io.segmentlist = NULL;
    }
    if (io.segmentmarkerlist)
    {
        free(io.segmentmarkerlist);
        io.segmentmarkerlist = NULL;
    }
    if (io.regionlist)
    {
        free(io.regionlist);
        io.regionlist = NULL;
    }
    if (io.holelist)
    {
        // free(io.holelist);
        // io.holelist = NULL;
    }
    if (io.neighborlist)
    {
        free(io.neighborlist);
        io.neighborlist = NULL;
    }
    if (io.edgelist)
    {
        free(io.edgelist);
        io.edgelist = NULL;
    }
    if (io.edgemarkerlist)
    {
        free(io.edgemarkerlist);
        io.edgemarkerlist = NULL;
    }
    if (io.trianglearealist)
    {
        free(io.trianglearealist);
        io.trianglearealist = NULL;
    }
    if (io.triangleattributelist)
    {
        free(io.triangleattributelist);
        io.triangleattributelist = NULL;
    }
    if (io.trianglelist)
    {
        free(io.trianglelist);
        io.trianglelist = NULL;
    }
    if (io.normlist)
    {
        free(io.normlist);
        io.normlist = NULL;
    }
}

Triangulator::Triangulator()
{
    quiet_ = false;
    convex_hull_ = false;
    comformming_delaunay_ = false;
    suppress_boundary_splitting_ = false;

    maximun_area_ = -1.0;
    minimun_angle_ = -1.0;
    maximum_steiner_points_ = -1;
}

Triangulator::~Triangulator()
{
}

void Triangulator::triangulatePolygon(const Polygon &poly, vector<Eigen::Vector3d> &verts, vector<Eigen::Vector3i> &faces)
{
    using namespace Eigen;

    //string cmd = "-pYzQ";  //Y: no Steiner points, z: index from zero, Q: quite
    cmd_ = getTriangleCmd();

    //0: init plane
    if (!init_plane(poly.bdryPts_))
        cout << "Error in Triangulator::triangulatePolygonWithHoles: [input points are not coplanar]" << endl;

    //1: init
    triangulateio input, output;
    initTriangulateio(input);
    initTriangulateio(output);

    input.numberofpoints = poly.ptsCnt_;
    input.numberofsegments = poly.ptsCnt_;
    input.pointlist = (REAL *)malloc((input.numberofpoints) * 2 * sizeof(REAL));
    input.segmentlist = (int *)malloc(input.numberofsegments * 2 * sizeof(int));
    int i = 0;
    for (i = 0; i < input.numberofpoints; i++)
    {
        Vector3d v = project_to_plane(poly.bdryPts_[i]);
        input.pointlist[2 * i] = v[0];
        input.pointlist[2 * i + 1] = v[1];
    }
    i = 0;
    for (i = 0; i < input.numberofsegments; i++)
    {
        input.segmentlist[2 * i] = i;
        input.segmentlist[2 * i + 1] = i + 1;
    }
    input.segmentlist[2 * i - 1] = 0;

    // if (input_edges.size() == input_boundary_markers.size())
    // {
    //     input.segmentmarkerlist = (int *)malloc(input.numberofsegments * sizeof(int));
    //     for (int i = 0; i < input_boundary_markers.size(); i++)
    //     {
    //         input.segmentmarkerlist[i] = input_boundary_markers[i];
    //     }
    // }

    //2: apply
    char *t_cmd = const_cast<char *>(cmd_.c_str());
    triangulate(t_cmd, &input, &output, (struct triangulateio *)NULL);

    //3: output
    verts.clear();
    faces.clear();
    for (int i = 0; i < output.numberofpoints; i++)
    {
        verts.push_back(unproject_to_plane(Vector3d(output.pointlist[2 * i], output.pointlist[2 * i + 1], 0)));
    }
    for (int i = 0; i < output.numberoftriangles; i++)
    {
        faces.push_back(Vector3i(output.trianglelist[3 * i], output.trianglelist[3 * i + 1], output.trianglelist[3 * i + 2]));
    }

    //4: free
    freeTriangualteio(input);
    freeTriangualteio(output);
}

void Triangulator::triangulatePolygonWithHoles(const Polygon &poly, const vector<Hole> holes, vector<Eigen::Vector3d> &verts, vector<Eigen::Vector3i> &faces)
{
    using namespace Eigen;
    //string cmd = "-pcYqzgQ";   //
    cmd_ = getTriangleCmd();

    //0: init plane
    if (!init_plane(poly.bdryPts_))
    {
        cout << "Error in Triangulator::triangulatePolygonWithHoles: [input points are not coplanar]" << endl;
    }

    //1: init
    triangulateio input, output;
    initTriangulateio(input);
    initTriangulateio(output);
    int holePtsCnt = 0;
    for (int i = 0; i < holes.size(); i++)
        holePtsCnt += holes[i].ptsCnt_;
    input.numberofpoints = poly.ptsCnt_ + holePtsCnt;
    input.numberofsegments = poly.ptsCnt_ + holePtsCnt;
    input.numberofholes = int(holes.size());
    input.pointlist = (REAL *)malloc((input.numberofpoints) * 2 * sizeof(REAL));
    input.segmentlist = (int *)malloc(input.numberofsegments * 2 * sizeof(int));
    input.holelist = (REAL *)malloc((input.numberofholes) * 2 * sizeof(REAL));
    int cnt = 0;
    for (int i = 0; i < poly.ptsCnt_; i++)
    {
        Vector3d v = project_to_plane(poly.bdryPts_[i]);
        input.pointlist[cnt] = v[0];
        input.pointlist[cnt + 1] = v[1];
        cnt = cnt + 2;
    }
    for (int i = 0; i < holes.size(); i++)
    {
        for (int j = 0; j < holes[i].ptsCnt_; j++)
        {
            Vector3d v = project_to_plane(holes[i].bdryPts_[j]);
            input.pointlist[cnt] = v[0];
            input.pointlist[cnt + 1] = v[1];
            cnt = cnt + 2;
        }
    }
    cnt = 0;
    for (int i = 0; i < poly.ptsCnt_; i++)
    {
        input.segmentlist[cnt] = i;
        input.segmentlist[cnt + 1] = i + 1;
        cnt = cnt + 2;
    }
    input.segmentlist[cnt - 1] = 0;
    for (int i = 0; i < holes.size(); i++)
    {
        int cnt_temp = cnt / 2;
        for (int j = 0; j < holes[i].ptsCnt_; j++)
        {
            input.segmentlist[cnt] = cnt_temp + j;
            input.segmentlist[cnt + 1] = cnt_temp + j + 1;
            cnt = cnt + 2;
        }
        input.segmentlist[cnt - 1] = cnt_temp;
    }
    cnt = 0;
    for (int i = 0; i < holes.size(); i++)
    {
        Vector3d v = project_to_plane(holes[i].getCenter());
        input.holelist[cnt] = v[0];
        input.holelist[cnt + 1] = v[1];
        cnt = cnt + 2;
    }

    // if (input_edges.size() == input_boundary_markers.size())
    // {
    //     input.segmentmarkerlist = (int *)malloc(input.numberofsegments * sizeof(int));
    //     for (int i = 0; i < input_boundary_markers.size(); i++)
    //     {
    //         input.segmentmarkerlist[i] = input_boundary_markers[i];
    //     }
    // }

    //2: apply
    char *t_cmd = const_cast<char *>(cmd_.c_str());
    triangulate(t_cmd, &input, &output, (struct triangulateio *)NULL);

    //3: output
    verts.clear();
    faces.clear();
    for (int i = 0; i < output.numberofpoints; i++)
    {
        verts.push_back(unproject_to_plane(Vector3d(output.pointlist[2 * i], output.pointlist[2 * i + 1], 0)));
    }
    for (int i = 0; i < output.numberoftriangles; i++)
    {
        faces.push_back(Vector3i(output.trianglelist[3 * i], output.trianglelist[3 * i + 1], output.trianglelist[3 * i + 2]));
    }

    //4: free
    freeTriangualteio(input);
    freeTriangualteio(output);
}

string Triangulator::getTriangleCmd()
{
    using std::stringstream;
    string cmd = "pz";

    if (minimun_angle_ > 0)
    {
        string str;
        stringstream ss;
        ss << minimun_angle_;
        ss >> str;
        cmd += "q" + str;
    }
    if (maximun_area_ > 0)
    {
        string str;
        stringstream ss;
        ss << maximun_area_;
        ss >> str;
        cmd += "a" + str;
    }
    if (convex_hull_)
    {
        cmd += "c";
    }
    if (comformming_delaunay_)
    {
        cmd += "D";
    }
    if (maximum_steiner_points_ > 0)
    {
        string str;
        stringstream ss;
        ss << maximum_steiner_points_;
        ss >> str;
        cmd += "S" + str;
    }
    if (quiet_)
    {
        cmd += "Q";
    }
    if (suppress_boundary_splitting_)
    {
        cmd += "Y";
    }

    return cmd;
}

bool Triangulator::init_plane(const vector<Eigen::Vector3d> &input_vertices_positions)
{
    using namespace Eigen;

    if (input_vertices_positions.size() <= 2)
        return false;

    // compute centroid of points
    Vector3d center(0.0f, 0.0f, 0.0f);
    for (int i = 0; i < input_vertices_positions.size(); i++)
        center = center + input_vertices_positions[i];
    plane_center_ = 1.0f / input_vertices_positions.size() * center;

    // determine the x-axis
    int x_id = -1;
    double max_dist = -std::numeric_limits<double>::infinity();
    for (int i = 0; i < input_vertices_positions.size(); i++)
    {
        plane_x_axis_ = input_vertices_positions[i] - plane_center_;
        if (plane_x_axis_.norm() > max_dist)
        {
            x_id = i;
            max_dist = plane_x_axis_.norm();
        }
    }
    plane_x_axis_ = input_vertices_positions[x_id] - plane_center_;
    plane_x_axis_.normalize();

    bool flag = false;
    Vector3d normal_tmp;
    double max_angle = -std::numeric_limits<double>::infinity();
    int y_id = -1;
    for (int i = 0; i < input_vertices_positions.size(); i++)
    {
        Vector3d v = input_vertices_positions[i] - plane_center_;
        v.normalize();
        normal_tmp = v.cross(plane_x_axis_);
        if (normal_tmp.norm() > max_angle)
        {
            max_angle = normal_tmp.norm();
            flag = true;
            y_id = i;
        }
    }
    normal_tmp = input_vertices_positions[y_id] - plane_center_;
    normal_tmp.normalize();
    if (flag)
    {
        plane_normal_ = normal_tmp.cross(plane_x_axis_);
        plane_normal_.normalize();
        plane_y_axis_ = plane_x_axis_.cross(plane_normal_);
        plane_y_axis_.normalize();
    }
    return flag;
}

Eigen::Vector3d Triangulator::project_to_plane(const Eigen::Vector3d &v)
{
    Eigen::Vector3d vout;
    vout[0] = (v - plane_center_).dot(plane_x_axis_);
    vout[1] = (v - plane_center_).dot(plane_y_axis_);
    return vout;
}

Eigen::Vector3d Triangulator::unproject_to_plane(const Eigen::Vector3d &v)
{
    Eigen::Vector3d vout = plane_center_ + plane_x_axis_ * v[0] + plane_y_axis_ * v[1];
    return vout;
}
