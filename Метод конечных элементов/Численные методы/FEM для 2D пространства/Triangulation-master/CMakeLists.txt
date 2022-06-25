cmake_minimum_required(VERSION 3.9)

set(PROJECT_NAME Triangulation)
project(${PROJECT_NAME})

set(CMAKE_CXX_STANDARD 11)

set(HEADER_PATH ${PROJECT_SOURCE_DIR}/include)
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${PROJECT_BINARY_DIR}/lib)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${PROJECT_BINARY_DIR}/lib)
set(CMAKE_EXECUTABLE_OUTPUT_PATH ${PROJECT_BINARY_DIR}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${PROJECT_BINARY_DIR}/bin)

# set cmake module path
list(APPEND CMAKE_MODULE_PATH ${PROJECT_SOURCE_DIR}/cmake)

include_directories(${HEADER_PATH})
include_directories(${PROJECT_SOURCE_DIR}/triangulation)
link_directories(${CMAKE_LIBRARY_OUTPUT_DIRECTORY})

option(BUILD_EXAMPLES "Build examples"         ON)

set(DATAPATH ${PROJECT_SOURCE_DIR}/data)
add_definitions("-DDATA_PATH=\"${DATAPATH}/\"")

add_subdirectory(triangulation)
if(BUILD_EXAMPLES)
    add_subdirectory(examples)
endif(BUILD_EXAMPLES)

