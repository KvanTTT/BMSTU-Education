// SLAU.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "conio.h"

using namespace std;

typedef unsigned short int byte;

const byte MAX_SIZE = 32;
const int DivisionByZero = 1;

enum SolMethod {Gauss, Kramer};

struct matrix
{
	double data[MAX_SIZE][MAX_SIZE];
	unsigned short int high;
};

struct vector
{
	double data[MAX_SIZE];
	byte high;
};

void enter_SLAU(matrix& coeffs, vector& free_terms);
void output_vector(vector& v);

void kramer(matrix &coeffs, vector &free_terms, vector &solution, int &error);
double det(matrix &m);
void swap_column(matrix &m, vector &ar, byte n);

void gauss(matrix &coeffs, vector &free_terms, vector &solution, int &error);
void find_sol(matrix &m, vector &sol);
int triangle(matrix &m);

int main()
{
	matrix coeffs;
	vector free_terms;
	vector solution_kramer;
	vector solution_gauss;
	int error;

	while (1)
	{
		enter_SLAU(coeffs, free_terms);

		kramer(coeffs, free_terms, solution_kramer, error);
		if (error)
			cout << "\nUnsolved system (det of coeffs matrix = 0)";
		else
		{
			cout << "\nSolution of system(Kramer): \n";
			output_vector(solution_kramer);
		}

		gauss(coeffs, free_terms, solution_gauss, error);
		if (error)
			cout << "\nUnsolved system (don't convert to triangle form)";
		else
		{		
			cout << "\nSolution of system(Gauss): \n";
			output_vector(solution_gauss);
		}
		
		cout << "\n\n--------------------------------\n\n";

	}

	getch();
}

inline byte ord(double x)
{
	if (x == 0)
		return 1;
	else
		return 0;
}

void swap_rows(matrix& m, byte r1, byte r2)
{
	double t;
	for (int i = 0; i <= m.high; i++)
	{
		t = m.data[r1][i];
		m.data[r1][i] = m.data[r2][i];
		m.data[r2][i] = t;
	}
}

double det(matrix& m)
{
	double result = 1;
	byte rows, cols;
	matrix a = m;
	int i, j, k;

	for (i=0; i<a.high; i++)
	{
		for (j=i; j<=a.high; j++)
		{
			rows = 0;
			cols = 0;
			for (k=i; k<=a.high; k++)
			{
				rows += ord(a.data[j][k]);
				cols += ord(a.data[k][j]);
			}
			if (rows + cols == 0)
				break;
			if ((cols == a.high - i + 1) || (rows == a.high - i + 1))
			{
				result = 0.0;
				return result;
			}
		}
		if (a.data[i, i] == 0)
			for (j = i+1; j <= a.high; j++)
				if (a.data[j][i] != 0.0)
				{
					result = -result;
					swap_rows(a, i, j); 
					break;
				}
		for (j=i+1; j <= a.high; j++)
			if (a.data[j][i] != 0.0)
			{
				for (k=i+1; k<=a.high; k++)
					a.data[j][k] = a.data[j][k] - a.data[i][k] * a.data[j][i] / a.data[i][i];
				a.data[j][i] = 0.0;
			}
	}
	for (i=0; i<=a.high; i++)
		result = result * a.data[i][i];
	return result;
}

void swap_column(matrix& m, vector& ar, byte n)
{
	double t;
	for (int i = 0; i <= m.high; i++)
	{
		t = m.data[i][n];
		m.data[i][n] = ar.data[i];
		ar.data[i] = t;
	}
}

void find_sol(matrix& m, vector& sol)
{
  double s;
  for(int i = m.high; i >= 0; i--)
  {
	s = 0;
	for(int j=i+1; j <= m.high; j++)
		s += m.data[i][j] * sol.data[j];
	sol.data[i] = m.data[i][m.high+1] - s;
  }
}

int triangle(matrix& m)
{
int q;
int i, j;

for(i=0; i <= m.high; i++)
{
	if (m.data[i][i]==0)
    {
		q = -1;
        for(j=i+1; j <= m.high; j++)
			if (m.data[j][j] != 0)
        {
			q = j;
			break;
		}
        if (q == -1) return 1;
        else
			swap_rows(m, i, q);
	}
	for(j = m.high+1; j >= i; j--)
      m.data[i][j] /= m.data[i][i];
	for(j = i+1; j <= m.high; j++)
      for(int k = m.high+1; k >= 0; k--)
        m.data[j][k] -= m.data[i][k] * m.data[j][i];
	}
	return 0;
}

void enter_SLAU(matrix& coeffs, vector& free_terms)
{
	cout << "Enter size of matrix (1..256): ";
	int k;
	cin >> k;
	k--;
	coeffs.high = k;
	free_terms.high = k;

	int i;
	int j;
	cout << "Enter matrix of coeffs: \n";
	for (i = 0; i <= coeffs.high; i++)
	{
		for (j = 0; j <= coeffs.high; j++)
		{
			cin >> coeffs.data[i][j];
		}
	}

	cout << "\nEnter column of free terms: \n";
	for (i = 0; i <= coeffs.high; i++)
		cin >> free_terms.data[i];
}

void kramer(matrix &coeffs, vector &free_terms, vector &solution, int &error)
{
		error = 0;
		double inv_det = det(coeffs);
		try
		{
			if (inv_det == 0)
				throw DivisionByZero;
			inv_det = 1/det(coeffs);
		}
		catch (int e)
		{
			if (e == DivisionByZero)
			{
				error = 1;
				exit;
			}
		}

		vector t = free_terms;
		solution.high = coeffs.high;
		for (int i = 0; i <= coeffs.high; i++)
		{
			swap_column(coeffs, t, i);
			solution.data[i] = inv_det * det(coeffs);
			swap_column(coeffs, t, i);
		}
}

void gauss(matrix &coeffs, vector &free_terms, vector &solution, int &error)
{
	matrix T = coeffs;
	for (int i = 0; i <= coeffs.high; i++)
		T.data[i][coeffs.high+1] = free_terms.data[i];
	error = triangle(T);
	solution.high = coeffs.high;
	find_sol(T, solution);
}

void output_vector(vector& v)
{
		for (int i = 0; i <= v.high; i++)
			cout << v.data[i] << " ";
}
