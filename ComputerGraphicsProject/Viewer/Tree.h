#pragma once

#include "vector.h"

class Tree
{
public:
	Tree(){};
	Tree(char *FileName, Vector Position);
	void Build(char *FileName, Vector Position);
	~Tree();
//private:
	Vector Pos;
	VertexData *Vertices;
	int VertexCount;
};