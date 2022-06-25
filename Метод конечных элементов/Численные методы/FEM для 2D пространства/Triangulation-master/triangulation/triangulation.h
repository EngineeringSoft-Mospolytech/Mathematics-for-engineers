#ifndef TRIANGULATION_H_
#define TRIANGULATION_H_

#include <vector>
#include <string>

#include <Eigen/Dense>

struct Polygon
{
    int ptsCnt_ = 0;
    std::vector<Eigen::Vector3d> bdryPts_;
    void addPt(const Eigen::Vector3d &p)
    {
        bdryPts_.push_back(p);
        ptsCnt_++;
    }

    /*! 
     * @brief Calculate the center of the polygon
     *
     * Used for `triangulatePolygonWithHoles` to compute the center to mark holes
     * @return the center of polygon 
     * @note  Since we just need to find a point in the polygon to mark it as a hole, 
     * any point in the polygon is ok. But when the polygon is not convex, the center may be out of the polygon.
     * The triangulation result will be wrong.
     * @see
    */
    Eigen::Vector3d getCenter() const
    {
        Eigen::Vector3d O = Eigen::Vector3d(0.0, 0.0, 0.0);
        for (auto p : bdryPts_)
            O += p;
        O /= float(ptsCnt_);
        return O;
    }
};

typedef Polygon Hole;

class Triangulator
{
public:
    Triangulator();
    ~Triangulator();

    /*! 
     * @brief Triangulate a polygon.
     *
     * @param poly input polygon
     * @param verts output mesh vertex
     * @param faces output mesh faces(triangles)
     * @return  
     * @note  
     * @see
    */
    void triangulatePolygon(const Polygon &poly, std::vector<Eigen::Vector3d> &verts, std::vector<Eigen::Vector3i> &faces);
    /*! 
     * @brief Triangulate a polygon with holes.
     *
     * Detailed explanation.
     * @param poly input polygon
     * @param holes input holes
     * @param verts output mesh vertex
     * @param faces output mesh faces(triangles)
     * @return  
     * @note  
     * @see
    */
    void triangulatePolygonWithHoles(const Polygon &poly, const std::vector<Hole> holes, std::vector<Eigen::Vector3d> &verts, std::vector<Eigen::Vector3i> &faces);

public:
    void set_cmd(const std::string &cmd) { cmd_ = cmd; }
    void set_quiet(bool t) { quiet_ = t; }
    void set_maximun_area(double area) { maximun_area_ = area; }
    void set_minimun_angle(double angle) { minimun_angle_ = angle; }
    void set_maximum_steiner_points(int n) { maximum_steiner_points_ = n; }
    void set_convex_hull(bool t) { convex_hull_ = t; }
    void set_comformming_delaunay(bool t) { comformming_delaunay_ = t; }
    void set_suppress_boundary_splitting(bool t) { suppress_boundary_splitting_ = t; }

    double get_maximun_area() const { return maximun_area_; }
    double get_minimum_angle() const { return minimun_angle_; }
    int get_maximum_steiner_points() const { return maximum_steiner_points_; }

    bool is_quiet() const { return quiet_; }
    bool is_convex_hull() const { return convex_hull_; }
    bool is_comformming_delaunay() const { return comformming_delaunay_; }
    bool is_suppress_boundary_splitting() const { return suppress_boundary_splitting_; }
    std::string get_cmd() const { return cmd_; }

private:
    std::string getTriangleCmd();
    bool init_plane(const std::vector<Eigen::Vector3d> &input_vertices_positions);
    Eigen::Vector3d project_to_plane(const Eigen::Vector3d &v);
    Eigen::Vector3d unproject_to_plane(const Eigen::Vector3d &v);

private:
    std::string cmd_;

    double maximun_area_;        ///* if -1, not use; if > 0, use it;
    double minimun_angle_;       ///* if -1, not use; if > 0, use it;
    int maximum_steiner_points_; ///* if -1, not use; if > 0, use it;
    bool convex_hull_;
    bool comformming_delaunay_;
    bool suppress_boundary_splitting_;
    bool quiet_;

    Eigen::Vector3d plane_normal_;
    Eigen::Vector3d plane_center_;
    Eigen::Vector3d plane_x_axis_;
    Eigen::Vector3d plane_y_axis_;
};

#endif // TRIANGULATION_H_
