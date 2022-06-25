cmake_minimum_required(VERSION 3.9)

set(TRIANGLE_LIB_SRC triangle/triangle.h triangle/triangle.c)
add_library(triangulation STATIC triangulation.h triangulation.cpp ${TRIANGLE_LIB_SRC})
target_compile_definitions(triangulation PRIVATE -DTRILIBRARY -DANSI_DECLARATORS)
if(WIN32)
   target_compile_definitions(triangulation PRIVATE -DNO_TIMER)
endif()