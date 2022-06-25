#pragma once
#include <Eigen/Dense>
#include <Eigen/Sparse>
#include <string>
#include <vector>
#include <iostream>
#include <fstream>

using namespace std;

struct Element;
struct Constraint;

extern "C" __declspec(dllexport) void Creat();