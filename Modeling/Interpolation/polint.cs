/*************************************************************************
Copyright (c) 2006-2009, Sergey Bochkanov (ALGLIB project).

>>> SOURCE LICENSE >>>
This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation (www.fsf.org); either version 2 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

A copy of the GNU General Public License is available at
http://www.fsf.org/licensing/licenses

>>> END OF LICENSE >>>
*************************************************************************/

using System;

namespace alglib
{
    public class polint
    {
        /*************************************************************************
        Polynomial fitting report:
            TaskRCond       reciprocal of task's condition number
            RMSError        RMS error
            AvgError        average error
            AvgRelError     average relative error (for non-zero Y[I])
            MaxError        maximum error
        *************************************************************************/
        public struct polynomialfitreport
        {
            public double taskrcond;
            public double rmserror;
            public double avgerror;
            public double avgrelerror;
            public double maxerror;
        };



        /*************************************************************************
        Lagrange intepolant: generation of the model on the general grid.
        This function has O(N^2) complexity.

        INPUT PARAMETERS:
            X   -   abscissas, array[0..N-1]
            Y   -   function values, array[0..N-1]
            N   -   number of points, N>=1

        OIYTPUT PARAMETERS
            P   -   barycentric model which represents Lagrange interpolant
                    (see ratint unit info and BarycentricCalc() description for
                    more information).

          -- ALGLIB --
             Copyright 02.12.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void polynomialbuild(ref double[] x,
            ref double[] y,
            int n,
            ref ratint.barycentricinterpolant p)
        {
            int j = 0;
            int k = 0;
            double[] w = new double[0];
            double b = 0;
            double a = 0;
            double v = 0;
            double mx = 0;
            int i_ = 0;

            System.Diagnostics.Debug.Assert(n > 0, "PolIntBuild: N<=0!");

            //
            // calculate W[j]
            // multi-pass algorithm is used to avoid overflow
            //
            w = new double[n];
            a = x[0];
            b = x[0];
            for (j = 0; j <= n - 1; j++)
            {
                w[j] = 1;
                a = Math.Min(a, x[j]);
                b = Math.Max(b, x[j]);
            }
            for (k = 0; k <= n - 1; k++)
            {

                //
                // W[K] is used instead of 0.0 because
                // cycle on J does not touch K-th element
                // and we MUST get maximum from ALL elements
                //
                mx = Math.Abs(w[k]);
                for (j = 0; j <= n - 1; j++)
                {
                    if (j != k)
                    {
                        v = (b - a) / (x[j] - x[k]);
                        w[j] = w[j] * v;
                        mx = Math.Max(mx, Math.Abs(w[j]));
                    }
                }
                if (k % 5 == 0)
                {

                    //
                    // every 5-th run we renormalize W[]
                    //
                    v = 1 / mx;
                    for (i_ = 0; i_ <= n - 1; i_++)
                    {
                        w[i_] = v * w[i_];
                    }
                }
            }
            ratint.barycentricbuildxyw(ref x, ref y, ref w, n, ref p);
        }


        /*************************************************************************
        Lagrange intepolant: generation of the model on equidistant grid.
        This function has O(N) complexity.

        INPUT PARAMETERS:
            A   -   left boundary of [A,B]
            B   -   right boundary of [A,B]
            Y   -   function values at the nodes, array[0..N-1]
            N   -   number of points, N>=1
                    for N=1 a constant model is constructed.

        OIYTPUT PARAMETERS
            P   -   barycentric model which represents Lagrange interpolant
                    (see ratint unit info and BarycentricCalc() description for
                    more information).

          -- ALGLIB --
             Copyright 03.12.2009 by Bochkanov Sergey
        *************************************************************************/
      /*  public static void polynomialbuildeqdist(double a,
            double b,
            ref double[] y,
            int n,
            ref ratint.barycentricinterpolant p)
        {
            int i = 0;
            double[] w = new double[0];
            double[] x = new double[0];
            double v = 0;

            System.Diagnostics.Debug.Assert(n > 0, "PolIntBuildEqDist: N<=0!");

            //
            // Special case: N=1
            //
            if (n == 1)
            {
                x = new double[1];
                w = new double[1];
                x[0] = 0.5 * (b + a);
                w[0] = 1;
                ratint.barycentricbuildxyw(ref x, ref y, ref w, 1, ref p);
                return;
            }

            //
            // general case
            //
            x = new double[n];
            w = new double[n];
            v = 1;
            for (i = 0; i <= n - 1; i++)
            {
                w[i] = v;
                x[i] = a + (b - a) * i / (n - 1);
                v = -(v * (n - 1 - i));
                v = v / (i + 1);
            }
            ratint.barycentricbuildxyw(ref x, ref y, ref w, n, ref p);
        }*/
    }
}
