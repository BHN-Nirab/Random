using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    //plane mesh 01
    public Mesh mesh01;

    //plane mesh 02
    public Mesh mesh02;

    //combine plane mesh 01 and plane mesh 02
    public Mesh combinedMesh;
    Vector3[] combinedMeshVertex;
    int[] combinedMeshTriangles;

    void Start()
    {

        //initialize combine mesh vertices and triangles with mesh01 vertices and traingles
        combinedMeshVertex = mesh01.vertices;
        combinedMeshTriangles = mesh01.triangles;

        //select which vertex should connect which vertex 
        int numberOfDemoJoinedVertex = 2;
        int[] demoJoinedVertexIndexFrom = { 1, 2 };
        int[] demoJoinedVertexIndexTo = { 0, 3 };

        //convert world location to local location
        for(int i=0; i<numberOfDemoJoinedVertex; i++)
        {
            int xDistance = yDistance = zDistance = 0;

            xDistance = mesh01.vertices[demoJoinedVertexIndexTo[i]][0] - mesh02.vertices[demoJoinedVertexIndexFrom[i]][0];
            yDistance = mesh01.vertices[demoJoinedVertexIndexTo[i]][1] - mesh02.vertices[demoJoinedVertexIndexFrom[i]][1];
            ZDistance = mesh01.vertices[demoJoinedVertexIndexTo[i]][2] - mesh02.vertices[demoJoinedVertexIndexFrom[i]][2];

            combinedMeshVertex[mesh01.vertices.Length+i][0] = mesh02.vertices[demoJoinedVertexIndexFrom[i]][0] + xDistance;
            combinedMeshVertex[mesh01.vertices.Length+i][1] = mesh02.vertices[demoJoinedVertexIndexFrom[i]][1] + yDistance;
            combinedMeshVertex[mesh01.vertices.Length+i][2] = mesh02.vertices[demoJoinedVertexIndexFrom[i]][2] + zDistance;
        }


        //select which vertex should connect which vertex 
        numberOfDemoJoinedVertex = 2;
        demoJoinedVertexIndexFrom = { 0, 3 };

        //make new vertex for demonstration purpose
        Vector3[] newPoint = { 
            (
            mesh01.vertices[demoJoinedVertexIndexTo[0]][0] + 0, 
            mesh01.vertices[demoJoinedVertexIndexTo[0]][1] + 0, 
            mesh01.vertices[demoJoinedVertexIndexTo[0]][2] - 2
            ),
            (
            mesh01.vertices[demoJoinedVertexIndexTo[1]][0] + 0,
            mesh01.vertices[demoJoinedVertexIndexTo[1]][1] + 0,
            mesh01.vertices[demoJoinedVertexIndexTo[1]][2] - 2
            ),
        };

        //convert world location to local location
        for (int i = 0; i < numberOfDemoJoinedVertex; i++)
        {
            int xDistance = yDistance = zDistance = 0;

            xDistance = newPoint.vertices[demoJoinedVertexIndexTo[i]][0] - mesh02.vertices[demoJoinedVertexIndexFrom[i]][0];
            yDistance = newPoint.vertices[demoJoinedVertexIndexTo[i]][1] - mesh02.vertices[demoJoinedVertexIndexFrom[i]][1];
            ZDistance = newPoint.vertices[demoJoinedVertexIndexTo[i]][2] - mesh02.vertices[demoJoinedVertexIndexFrom[i]][2];

            combinedMeshVertex[mesh01.vertices.Length + i][0] = mesh02.vertices[demoJoinedVertexIndexFrom[i]][0] + xDistance;
            combinedMeshVertex[mesh01.vertices.Length + i][1] = mesh02.vertices[demoJoinedVertexIndexFrom[i]][1] + yDistance;
            combinedMeshVertex[mesh01.vertices.Length + i][2] = mesh02.vertices[demoJoinedVertexIndexFrom[i]][2] + zDistance;
        }

        //connect vertex in clockwise direction
        int[] newTriangles = { 6,4,7,7,4,5};
        int j = 0;

        for (int i = mesh01.triangles.Length; i < mesh01.triangles.Length + newTriangles.Length; i++)
            combinedMeshTriangles[i] = newTriangles[j++];

        combineMesh();
    }

    private void combineMesh()
    {
        combinedMesh.Clear();
        combinedMesh.vertices = combinedMeshVertex;
        combinedMeshVertex.triangles = combinedMeshTriangles;
        combinedMeshVertex.RecalculateNormals();

    }
}
