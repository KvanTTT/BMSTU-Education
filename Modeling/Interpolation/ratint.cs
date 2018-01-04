/*************************************************************************
Copyright (c) 2007-2009, Sergey Bochkanov (ALGLIB project).

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
    public class ratint
    {
        /*************************************************************************
        Barycentric interpolant.
        *************************************************************************/
        public struct barycentricinterpolant
        {
            public int n;
            public double sy;
            public double[] x;
            public double[] y;
            public double[] w;
        };


        /*************************************************************************
        Barycentric fitting report:
            TaskRCond       reciprocal of task's condition number
            RMSError        RMS error
            AvgError        average error
            AvgRelError     average relative error (for non-zero Y[I])
            MaxError        maximum error
        *************************************************************************/
        public struct barycentricfitreport
        {
            public double taskrcond;
            public int dbest;
            public double rmserror;
            public double avgerror;
            public double avgrelerror;
            public double maxerror;
        };




        public const int brcvnum = 10;


        /*************************************************************************
        Rational interpolation using barycentric formula

        F(t) = SUM(i=0,n-1,w[i]*f[i]/(t-x[i])) / SUM(i=0,n-1,w[i]/(t-x[i]))

        Input parameters:
            B   -   barycentric interpolant built with one of model building
                    subroutines.
            T   -   interpolation point

        Result:
            barycentric interpolant F(t)

          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static double barycentriccalc(ref barycentricinterpolant b,
            double t)
        {
            double result = 0;
            double s1 = 0;
            double s2 = 0;
            double s = 0;
            double v = 0;
            int i = 0;

            
            //
            // special case: N=1
            //
            if( b.n==1 )
            {
                result = b.sy*b.y[0];
                return result;
            }
            
            //
            // Here we assume that task is normalized, i.e.:
            // 1. abs(Y[i])<=1
            // 2. abs(W[i])<=1
            // 3. X[] is ordered
            //
            s = Math.Abs(t-b.x[0]);
            for(i=0; i<=b.n-1; i++)
            {
                v = b.x[i];
                if( (double)(v)==(double)(t) )
                {
                    result = b.sy*b.y[i];
                    return result;
                }
                v = Math.Abs(t-v);
                if( (double)(v)<(double)(s) )
                {
                    s = v;
                }
            }
            s1 = 0;
            s2 = 0;
            for(i=0; i<=b.n-1; i++)
            {
                v = s/(t-b.x[i]);
                v = v*b.w[i];
                s1 = s1+v*b.y[i];
                s2 = s2+v;
            }
            result = b.sy*s1/s2;
            return result;
        }


        /*************************************************************************
        Differentiation of barycentric interpolant: first derivative.

        Algorithm used in this subroutine is very robust and should not fail until
        provided with values too close to MaxRealNumber  (usually  MaxRealNumber/N
        or greater will overflow).

        INPUT PARAMETERS:
            B   -   barycentric interpolant built with one of model building
                    subroutines.
            T   -   interpolation point

        OUTPUT PARAMETERS:
            F   -   barycentric interpolant at T
            DF  -   first derivative
            
        NOTE


          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentricdiff1(ref barycentricinterpolant b,
            double t,
            ref double f,
            ref double df)
        {
            double v = 0;
            double vv = 0;
            int i = 0;
            int k = 0;
            double n0 = 0;
            double n1 = 0;
            double d0 = 0;
            double d1 = 0;
            double s0 = 0;
            double s1 = 0;
            double xk = 0;
            double xi = 0;
            double xmin = 0;
            double xmax = 0;
            double xscale1 = 0;
            double xoffs1 = 0;
            double xscale2 = 0;
            double xoffs2 = 0;
            double xprev = 0;

            
            //
            // special case: N=1
            //
            if( b.n==1 )
            {
                f = b.sy*b.y[0];
                df = 0;
                return;
            }
            if( (double)(b.sy)==(double)(0) )
            {
                f = 0;
                df = 0;
                return;
            }
            System.Diagnostics.Debug.Assert((double)(b.sy)>(double)(0), "BarycentricDiff1: internal error");
            
            //
            // We assume than N>1 and B.SY>0. Find:
            // 1. pivot point (X[i] closest to T)
            // 2. width of interval containing X[i]
            //
            v = Math.Abs(b.x[0]-t);
            k = 0;
            xmin = b.x[0];
            xmax = b.x[0];
            for(i=1; i<=b.n-1; i++)
            {
                vv = b.x[i];
                if( (double)(Math.Abs(vv-t))<(double)(v) )
                {
                    v = Math.Abs(vv-t);
                    k = i;
                }
                xmin = Math.Min(xmin, vv);
                xmax = Math.Max(xmax, vv);
            }
            
            //
            // pivot point found, calculate dNumerator and dDenominator
            //
            xscale1 = 1/(xmax-xmin);
            xoffs1 = -(xmin/(xmax-xmin))+1;
            xscale2 = 2;
            xoffs2 = -3;
            t = t*xscale1+xoffs1;
            t = t*xscale2+xoffs2;
            xk = b.x[k];
            xk = xk*xscale1+xoffs1;
            xk = xk*xscale2+xoffs2;
            v = t-xk;
            n0 = 0;
            n1 = 0;
            d0 = 0;
            d1 = 0;
            xprev = -2;
            for(i=0; i<=b.n-1; i++)
            {
                xi = b.x[i];
                xi = xi*xscale1+xoffs1;
                xi = xi*xscale2+xoffs2;
                System.Diagnostics.Debug.Assert((double)(xi)>(double)(xprev), "BarycentricDiff1: points are too close!");
                xprev = xi;
                if( i!=k )
                {
                    vv = AP.Math.Sqr(t-xi);
                    s0 = (t-xk)/(t-xi);
                    s1 = (xk-xi)/vv;
                }
                else
                {
                    s0 = 1;
                    s1 = 0;
                }
                vv = b.w[i]*b.y[i];
                n0 = n0+s0*vv;
                n1 = n1+s1*vv;
                vv = b.w[i];
                d0 = d0+s0*vv;
                d1 = d1+s1*vv;
            }
            f = b.sy*n0/d0;
            df = (n1*d0-n0*d1)/AP.Math.Sqr(d0);
            if( (double)(df)!=(double)(0) )
            {
                df = Math.Sign(df)*Math.Exp(Math.Log(Math.Abs(df))+Math.Log(b.sy)+Math.Log(xscale1)+Math.Log(xscale2));
            }
        }


        /*************************************************************************
        Differentiation of barycentric interpolant: first/second derivatives.

        INPUT PARAMETERS:
            B   -   barycentric interpolant built with one of model building
                    subroutines.
            T   -   interpolation point

        OUTPUT PARAMETERS:
            F   -   barycentric interpolant at T
            DF  -   first derivative
            D2F -   second derivative

        NOTE: this algorithm may fail due to overflow/underflor if  used  on  data
        whose values are close to MaxRealNumber or MinRealNumber.  Use more robust
        BarycentricDiff1() subroutine in such cases.


          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentricdiff2(ref barycentricinterpolant b,
            double t,
            ref double f,
            ref double df,
            ref double d2f)
        {
            double v = 0;
            double vv = 0;
            int i = 0;
            int k = 0;
            double n0 = 0;
            double n1 = 0;
            double n2 = 0;
            double d0 = 0;
            double d1 = 0;
            double d2 = 0;
            double s0 = 0;
            double s1 = 0;
            double s2 = 0;
            double xk = 0;
            double xi = 0;

            f = 0;
            df = 0;
            d2f = 0;
            
            //
            // special case: N=1
            //
            if( b.n==1 )
            {
                f = b.sy*b.y[0];
                df = 0;
                d2f = 0;
                return;
            }
            if( (double)(b.sy)==(double)(0) )
            {
                f = 0;
                df = 0;
                d2f = 0;
                return;
            }
            System.Diagnostics.Debug.Assert((double)(b.sy)>(double)(0), "BarycentricDiff: internal error");
            
            //
            // We assume than N>1 and B.SY>0. Find:
            // 1. pivot point (X[i] closest to T)
            // 2. width of interval containing X[i]
            //
            v = Math.Abs(b.x[0]-t);
            k = 0;
            for(i=1; i<=b.n-1; i++)
            {
                vv = b.x[i];
                if( (double)(Math.Abs(vv-t))<(double)(v) )
                {
                    v = Math.Abs(vv-t);
                    k = i;
                }
            }
            
            //
            // pivot point found, calculate dNumerator and dDenominator
            //
            xk = b.x[k];
            v = t-xk;
            n0 = 0;
            n1 = 0;
            n2 = 0;
            d0 = 0;
            d1 = 0;
            d2 = 0;
            for(i=0; i<=b.n-1; i++)
            {
                if( i!=k )
                {
                    xi = b.x[i];
                    vv = AP.Math.Sqr(t-xi);
                    s0 = (t-xk)/(t-xi);
                    s1 = (xk-xi)/vv;
                    s2 = -(2*(xk-xi)/(vv*(t-xi)));
                }
                else
                {
                    s0 = 1;
                    s1 = 0;
                    s2 = 0;
                }
                vv = b.w[i]*b.y[i];
                n0 = n0+s0*vv;
                n1 = n1+s1*vv;
                n2 = n2+s2*vv;
                vv = b.w[i];
                d0 = d0+s0*vv;
                d1 = d1+s1*vv;
                d2 = d2+s2*vv;
            }
            f = b.sy*n0/d0;
            df = b.sy*(n1*d0-n0*d1)/AP.Math.Sqr(d0);
            d2f = b.sy*((n2*d0-n0*d2)*AP.Math.Sqr(d0)-(n1*d0-n0*d1)*2*d0*d1)/AP.Math.Sqr(AP.Math.Sqr(d0));
        }


        /*************************************************************************
        This subroutine performs linear transformation of the argument.

        INPUT PARAMETERS:
            B       -   rational interpolant in barycentric form
            CA, CB  -   transformation coefficients: x = CA*t + CB

        OUTPUT PARAMETERS:
            B       -   transformed interpolant with X replaced by T

          -- ALGLIB PROJECT --
             Copyright 19.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentriclintransx(ref barycentricinterpolant b,
            double ca,
            double cb)
        {
            int i = 0;
            int j = 0;
            double v = 0;

            
            //
            // special case, replace by constant F(CB)
            //
            if( (double)(ca)==(double)(0) )
            {
                b.sy = barycentriccalc(ref b, cb);
                v = 1;
                for(i=0; i<=b.n-1; i++)
                {
                    b.y[i] = 1;
                    b.w[i] = v;
                    v = -v;
                }
                return;
            }
            
            //
            // general case: CA<>0
            //
            for(i=0; i<=b.n-1; i++)
            {
                b.x[i] = (b.x[i]-cb)/ca;
            }
            if( (double)(ca)<(double)(0) )
            {
                for(i=0; i<=b.n-1; i++)
                {
                    if( i<b.n-1-i )
                    {
                        j = b.n-1-i;
                        v = b.x[i];
                        b.x[i] = b.x[j];
                        b.x[j] = v;
                        v = b.y[i];
                        b.y[i] = b.y[j];
                        b.y[j] = v;
                        v = b.w[i];
                        b.w[i] = b.w[j];
                        b.w[j] = v;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        /*************************************************************************
        This  subroutine   performs   linear  transformation  of  the  barycentric
        interpolant.

        INPUT PARAMETERS:
            B       -   rational interpolant in barycentric form
            CA, CB  -   transformation coefficients: B2(x) = CA*B(x) + CB

        OUTPUT PARAMETERS:
            B       -   transformed interpolant

          -- ALGLIB PROJECT --
             Copyright 19.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentriclintransy(ref barycentricinterpolant b,
            double ca,
            double cb)
        {
            int i = 0;
            double v = 0;
            int i_ = 0;

            for(i=0; i<=b.n-1; i++)
            {
                b.y[i] = ca*b.sy*b.y[i]+cb;
            }
            b.sy = 0;
            for(i=0; i<=b.n-1; i++)
            {
                b.sy = Math.Max(b.sy, Math.Abs(b.y[i]));
            }
            if( (double)(b.sy)>(double)(0) )
            {
                v = 1/b.sy;
                for(i_=0; i_<=b.n-1;i_++)
                {
                    b.y[i_] = v*b.y[i_];
                }
            }
        }


        /*************************************************************************
        Extracts X/Y/W arrays from rational interpolant

        INPUT PARAMETERS:
            B   -   barycentric interpolant

        OUTPUT PARAMETERS:
            N   -   nodes count, N>0
            X   -   interpolation nodes, array[0..N-1]
            F   -   function values, array[0..N-1]
            W   -   barycentric weights, array[0..N-1]

          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentricunpack(ref barycentricinterpolant b,
            ref int n,
            ref double[] x,
            ref double[] y,
            ref double[] w)
        {
            double v = 0;
            int i_ = 0;

            n = b.n;
            x = new double[n];
            y = new double[n];
            w = new double[n];
            v = b.sy;
            for(i_=0; i_<=n-1;i_++)
            {
                x[i_] = b.x[i_];
            }
            for(i_=0; i_<=n-1;i_++)
            {
                y[i_] = v*b.y[i_];
            }
            for(i_=0; i_<=n-1;i_++)
            {
                w[i_] = b.w[i_];
            }
        }


        /*************************************************************************
        Serialization of the barycentric interpolant

        INPUT PARAMETERS:
            B   -   barycentric interpolant

        OUTPUT PARAMETERS:
            RA      -   array of real numbers which contains interpolant,
                        array[0..RLen-1]
            RLen    -   RA lenght

          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentricserialize(ref barycentricinterpolant b,
            ref double[] ra,
            ref int ralen)
        {
            int i_ = 0;
            int i1_ = 0;

            ralen = 2+2+3*b.n;
            ra = new double[ralen];
            ra[0] = ralen;
            ra[1] = brcvnum;
            ra[2] = b.n;
            ra[3] = b.sy;
            i1_ = (0) - (4);
            for(i_=4; i_<=4+b.n-1;i_++)
            {
                ra[i_] = b.x[i_+i1_];
            }
            i1_ = (0) - (4+b.n);
            for(i_=4+b.n; i_<=4+2*b.n-1;i_++)
            {
                ra[i_] = b.y[i_+i1_];
            }
            i1_ = (0) - (4+2*b.n);
            for(i_=4+2*b.n; i_<=4+3*b.n-1;i_++)
            {
                ra[i_] = b.w[i_+i1_];
            }
        }


        /*************************************************************************
        Unserialization of the barycentric interpolant

        INPUT PARAMETERS:
            RA  -   array of real numbers which contains interpolant,

        OUTPUT PARAMETERS:
            B   -   barycentric interpolant

          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentricunserialize(ref double[] ra,
            ref barycentricinterpolant b)
        {
            int i_ = 0;
            int i1_ = 0;

            System.Diagnostics.Debug.Assert((int)Math.Round(ra[1])==brcvnum, "BarycentricUnserialize: corrupted array!");
            b.n = (int)Math.Round(ra[2]);
            b.sy = ra[3];
            b.x = new double[b.n];
            b.y = new double[b.n];
            b.w = new double[b.n];
            i1_ = (4) - (0);
            for(i_=0; i_<=b.n-1;i_++)
            {
                b.x[i_] = ra[i_+i1_];
            }
            i1_ = (4+b.n) - (0);
            for(i_=0; i_<=b.n-1;i_++)
            {
                b.y[i_] = ra[i_+i1_];
            }
            i1_ = (4+2*b.n) - (0);
            for(i_=0; i_<=b.n-1;i_++)
            {
                b.w[i_] = ra[i_+i1_];
            }
        }


        /*************************************************************************
        Copying of the barycentric interpolant

        INPUT PARAMETERS:
            B   -   barycentric interpolant

        OUTPUT PARAMETERS:
            B2  -   copy(B1)

          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentriccopy(ref barycentricinterpolant b,
            ref barycentricinterpolant b2)
        {
            int i_ = 0;

            b2.n = b.n;
            b2.sy = b.sy;
            b2.x = new double[b2.n];
            b2.y = new double[b2.n];
            b2.w = new double[b2.n];
            for(i_=0; i_<=b2.n-1;i_++)
            {
                b2.x[i_] = b.x[i_];
            }
            for(i_=0; i_<=b2.n-1;i_++)
            {
                b2.y[i_] = b.y[i_];
            }
            for(i_=0; i_<=b2.n-1;i_++)
            {
                b2.w[i_] = b.w[i_];
            }
        }


        /*************************************************************************
        Rational interpolant from X/Y/W arrays

        F(t) = SUM(i=0,n-1,w[i]*f[i]/(t-x[i])) / SUM(i=0,n-1,w[i]/(t-x[i]))

        INPUT PARAMETERS:
            X   -   interpolation nodes, array[0..N-1]
            F   -   function values, array[0..N-1]
            W   -   barycentric weights, array[0..N-1]
            N   -   nodes count, N>0

        OUTPUT PARAMETERS:
            B   -   barycentric interpolant built from (X, Y, W)

          -- ALGLIB --
             Copyright 17.08.2009 by Bochkanov Sergey
        *************************************************************************/
        public static void barycentricbuildxyw(ref double[] x,
            ref double[] y,
            ref double[] w,
            int n,
            ref barycentricinterpolant b)
        {
            int i_ = 0;

            System.Diagnostics.Debug.Assert(n>0, "BarycentricBuildXYW: incorrect N!");
            
            //
            // fill X/Y/W
            //
            b.x = new double[n];
            b.y = new double[n];
            b.w = new double[n];
            for(i_=0; i_<=n-1;i_++)
            {
                b.x[i_] = x[i_];
            }
            for(i_=0; i_<=n-1;i_++)
            {
                b.y[i_] = y[i_];
            }
            for(i_=0; i_<=n-1;i_++)
            {
                b.w[i_] = w[i_];
            }
            b.n = n;
            
            //
            // Normalize
            //
            barycentricnormalize(ref b);
        }

        /*************************************************************************
       Normalization of barycentric interpolant:
       * B.N, B.X, B.Y and B.W are initialized
       * B.SY is NOT initialized
       * Y[] is normalized, scaling coefficient is stored in B.SY
       * W[] is normalized, no scaling coefficient is stored
       * X[] is sorted

       Internal subroutine.
       *************************************************************************/
        private static void barycentricnormalize(ref barycentricinterpolant b)
        {
            int[] p1 = new int[0];
            int[] p2 = new int[0];
            int i = 0;
            int j = 0;
            int j2 = 0;
            double v = 0;
            int i_ = 0;


            //
            // Normalize task: |Y|<=1, |W|<=1, sort X[]
            //
            b.sy = 0;
            for (i = 0; i <= b.n - 1; i++)
            {
                b.sy = Math.Max(b.sy, Math.Abs(b.y[i]));
            }
            if ((double)(b.sy) > (double)(0) & (double)(Math.Abs(b.sy - 1)) > (double)(10 * AP.Math.MachineEpsilon))
            {
                v = 1 / b.sy;
                for (i_ = 0; i_ <= b.n - 1; i_++)
                {
                    b.y[i_] = v * b.y[i_];
                }
            }
            v = 0;
            for (i = 0; i <= b.n - 1; i++)
            {
                v = Math.Max(v, Math.Abs(b.w[i]));
            }
            if ((double)(v) > (double)(0) & (double)(Math.Abs(v - 1)) > (double)(10 * AP.Math.MachineEpsilon))
            {
                v = 1 / v;
                for (i_ = 0; i_ <= b.n - 1; i_++)
                {
                    b.w[i_] = v * b.w[i_];
                }
            }
            for (i = 0; i <= b.n - 2; i++)
            {
                if ((double)(b.x[i + 1]) < (double)(b.x[i]))
                {
                    tsort.tagsort(ref b.x, b.n, ref p1, ref p2);
                    for (j = 0; j <= b.n - 1; j++)
                    {
                        j2 = p2[j];
                        v = b.y[j];
                        b.y[j] = b.y[j2];
                        b.y[j2] = v;
                        v = b.w[j];
                        b.w[j] = b.w[j2];
                        b.w[j2] = v;
                    }
                    break;
                }
            }
        }
    }
}
