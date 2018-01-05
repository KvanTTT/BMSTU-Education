using System;
using MusicNotationLib;
using System.Collections.Generic;

namespace KvantSound
{
	public class NoteState
	{
		double Pitch;
		MusicSymbolDuration Duration;
	}

	public class MusicNotationFollower : MusicNotation
	{
		public override MusicNotationMode Mode
		{
			get
			{
				return MusicNotationMode.Follow;
			}
		}

		#region Tempo agent

		public NoteState[] MarkovChain;
		
		double eta_s, eta_phi;
		double tn, tn1;
		double r;
		double psi_k, psi_n, psi_n1, psi_n_p_1;
		double phi_n_, phi_n1_;
		double phi_n, phi_n1;
		double k;

		private static void besselm1firstcheb(double c, ref double b0, ref double b1, ref double b2)
		{
			b0 = c;
			b1 = 0.0;
			b2 = 0.0;
		}

		private static void besselm1nextcheb(double x, double c, ref double b0, ref double b1, ref double b2)
		{
			b2 = b1;
			b1 = b0;
			b0 = x * b1 - b2 + c;
		}

		private static void besselmfirstcheb(double c, ref double b0, ref double b1, ref double b2)
		{
			b0 = c;
			b1 = 0.0;
			b2 = 0.0;
		}

		private static void besselmnextcheb(double x, double c, ref double b0, ref double b1, ref double b2)
		{
			b2 = b1;
			b1 = b0;
			b0 = x * b1 - b2 + c;
		}

		public static double besseli0(double x)
		{
			double result = 0;
			double y = 0;
			double v = 0;
			double z = 0;
			double b0 = 0;
			double b1 = 0;
			double b2 = 0;

			if ((double)(x) < (double)(0))
			{
				x = -x;
			}
			if ((double)(x) <= (double)(8.0))
			{
				y = x / 2.0 - 2.0;
				besselmfirstcheb(-4.41534164647933937950E-18, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 3.33079451882223809783E-17, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -2.43127984654795469359E-16, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.71539128555513303061E-15, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -1.16853328779934516808E-14, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 7.67618549860493561688E-14, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -4.85644678311192946090E-13, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 2.95505266312963983461E-12, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -1.72682629144155570723E-11, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 9.67580903537323691224E-11, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -5.18979560163526290666E-10, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 2.65982372468238665035E-9, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -1.30002500998624804212E-8, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 6.04699502254191894932E-8, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -2.67079385394061173391E-7, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.11738753912010371815E-6, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -4.41673835845875056359E-6, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.64484480707288970893E-5, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -5.75419501008210370398E-5, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.88502885095841655729E-4, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -5.76375574538582365885E-4, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.63947561694133579842E-3, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -4.32430999505057594430E-3, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.05464603945949983183E-2, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -2.37374148058994688156E-2, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 4.93052842396707084878E-2, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -9.49010970480476444210E-2, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 1.71620901522208775349E-1, ref b0, ref b1, ref b2);
				besselmnextcheb(y, -3.04682672343198398683E-1, ref b0, ref b1, ref b2);
				besselmnextcheb(y, 6.76795274409476084995E-1, ref b0, ref b1, ref b2);
				v = 0.5 * (b0 - b2);
				result = Math.Exp(x) * v;
				return result;
			}
			z = 32.0 / x - 2.0;
			besselmfirstcheb(-7.23318048787475395456E-18, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -4.83050448594418207126E-18, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 4.46562142029675999901E-17, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 3.46122286769746109310E-17, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -2.82762398051658348494E-16, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -3.42548561967721913462E-16, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 1.77256013305652638360E-15, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 3.81168066935262242075E-15, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -9.55484669882830764870E-15, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -4.15056934728722208663E-14, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 1.54008621752140982691E-14, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 3.85277838274214270114E-13, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 7.18012445138366623367E-13, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -1.79417853150680611778E-12, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -1.32158118404477131188E-11, ref b0, ref b1, ref b2);
			besselmnextcheb(z, -3.14991652796324136454E-11, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 1.18891471078464383424E-11, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 4.94060238822496958910E-10, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 3.39623202570838634515E-9, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 2.26666899049817806459E-8, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 2.04891858946906374183E-7, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 2.89137052083475648297E-6, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 6.88975834691682398426E-5, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 3.36911647825569408990E-3, ref b0, ref b1, ref b2);
			besselmnextcheb(z, 8.04490411014108831608E-1, ref b0, ref b1, ref b2);
			v = 0.5 * (b0 - b2);
			result = Math.Exp(x) * v / Math.Sqrt(x);
			return result;
		}

		public static double besseli1(double x)
		{
			double result = 0;
			double y = 0;
			double z = 0;
			double v = 0;
			double b0 = 0;
			double b1 = 0;
			double b2 = 0;

			z = Math.Abs(x);
			if ((double)(z) <= (double)(8.0))
			{
				y = z / 2.0 - 2.0;
				besselm1firstcheb(2.77791411276104639959E-18, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -2.11142121435816608115E-17, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 1.55363195773620046921E-16, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.10559694773538630805E-15, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 7.60068429473540693410E-15, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -5.04218550472791168711E-14, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 3.22379336594557470981E-13, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.98397439776494371520E-12, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 1.17361862988909016308E-11, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -6.66348972350202774223E-11, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 3.62559028155211703701E-10, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.88724975172282928790E-9, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 9.38153738649577178388E-9, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -4.44505912879632808065E-8, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 2.00329475355213526229E-7, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -8.56872026469545474066E-7, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 3.47025130813767847674E-6, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.32731636560394358279E-5, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 4.78156510755005422638E-5, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.61760815825896745588E-4, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 5.12285956168575772895E-4, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.51357245063125314899E-3, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 4.15642294431288815669E-3, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.05640848946261981558E-2, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 2.47264490306265168283E-2, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -5.29459812080949914269E-2, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 1.02643658689847095384E-1, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.76416518357834055153E-1, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 2.52587186443633654823E-1, ref b0, ref b1, ref b2);
				v = 0.5 * (b0 - b2);
				z = v * z * Math.Exp(z);
			}
			else
			{
				y = 32.0 / z - 2.0;
				besselm1firstcheb(7.51729631084210481353E-18, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 4.41434832307170791151E-18, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -4.65030536848935832153E-17, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -3.20952592199342395980E-17, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 2.96262899764595013876E-16, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 3.30820231092092828324E-16, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.88035477551078244854E-15, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -3.81440307243700780478E-15, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 1.04202769841288027642E-14, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 4.27244001671195135429E-14, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -2.10154184277266431302E-14, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -4.08355111109219731823E-13, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -7.19855177624590851209E-13, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 2.03562854414708950722E-12, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 1.41258074366137813316E-11, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 3.25260358301548823856E-11, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.89749581235054123450E-11, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -5.58974346219658380687E-10, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -3.83538038596423702205E-9, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -2.63146884688951950684E-8, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -2.51223623787020892529E-7, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -3.88256480887769039346E-6, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -1.10588938762623716291E-4, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, -9.76109749136146840777E-3, ref b0, ref b1, ref b2);
				besselm1nextcheb(y, 7.78576235018280120474E-1, ref b0, ref b1, ref b2);
				v = 0.5 * (b0 - b2);
				z = v * Math.Exp(z) / Math.Sqrt(z);
			}
			if ((double)(x) < (double)(0))
			{
				z = -z;
			}
			result = z;
			return result;
		}

		public static double A(double x)
		{
			var result = besseli1(x) / besseli0(x);
			return result;
		}

		public static double InvA(double x)
		{
			double k;
			double Ax = A(x);
			k = Ax * (2 - Ax * Ax) / (1 - Ax * Ax);
			double A1x = A(k);
			k = k - (A1x - Ax) / (1 - A1x * A1x - A1x / k);
			A1x = A(k);
			k = k - (A1x - Ax) / (1 - A1x * A1x - A1x / k);
			
			return k;
		}

		public static double F(double phi, double phi_mu, double k)
		{
			return Math.Exp(k * Math.Cos(Math.PI * 2 * (phi - phi_mu))) * 
				Math.Sin(Math.PI * 2 * (phi - phi_mu)) /
				(Math.PI * 2 * Math.Exp(k));
		}

		public static double ModPI(double x)
		{
			x += Math.PI;
			x = x - (int)(x / (Math.PI * 2)) * Math.PI * 2;
			x -= Math.PI;

			return x;
		}

		public void UpdateTempo()
		{
			r = r - eta_s * (r -
				Math.Cos(Math.PI * 2 * ((tn - tn1) / psi_k - phi_n_)));

			k = InvA(r);

			phi_n = phi_n1 + (tn - tn1) / psi_n1 + eta_phi * ModPI(F(phi_n1, phi_n1_, k));

			psi_n_p_1 = psi_n * (1 + eta_s * F(phi_n, phi_n_, k));
		}

		#endregion

		#region Delegates

		public UpdateMuscialSymbolDelegate FollowMuscialSymbol
		{
			get;
			set;
		}

		#endregion

		#region Properties

		protected int CurrentSymbolIndex
		{
			get;
			set;
		}

		protected int NextSymbolIndex
		{
			get;
			set;
		}

		public MusicalSymbol CurrentSymbol
		{
			get;
			private set;
		}

		public MusicalSymbol NextSymbol
		{
			get;
			private set;
		}

		protected List<MusicalSymbol> MusicalSymbols;

		#endregion

		protected long LastNotationNoteDuration;

		private int FindNextMusicalIndex(int index)
		{
			int result = index;
			if (MusicalSymbols != null)
			for (int i = index + 1; i < MusicalSymbols.Count; i++)
				if (MusicalSymbols[i].Type == MusicSymbolType.Rest ||
					MusicalSymbols[i].Type == MusicSymbolType.Note)
				{
					result = i;
					break;
				}
			return result;
		}

		internal override void Process(Sample Sample)
		{
			Samples.Add(Sample);

			if (MusicalSymbols[CurrentSymbolIndex] is Note)
			{
				Note Note = MusicalSymbols[CurrentSymbolIndex] as Note;
				if (Sample.ID == Note.MidiPitch)
				{
					LastNoteDuration += Sample.Duration;

					if ((double)LastNoteDuration / LastNotationNoteDuration > 0.7)
					{
						CurrentSymbolIndex = FindNextMusicalIndex(CurrentSymbolIndex);
						FollowMuscialSymbol(MusicalSymbols[CurrentSymbolIndex]);
						LastNoteDuration = 0;
						LastNotationNoteDuration = GetDuration(MusicalSymbols[CurrentSymbolIndex]);
					}
				}
			}
			else
				if (MusicalSymbols[CurrentSymbolIndex] is Rest)
				{
					if (Sample.Silent)
					{
						LastNoteDuration += Sample.Duration;

						if ((double)LastNoteDuration / LastNotationNoteDuration > 0.7)
						{
							CurrentSymbolIndex = FindNextMusicalIndex(CurrentSymbolIndex);
							FollowMuscialSymbol(MusicalSymbols[CurrentSymbolIndex]);
							LastNoteDuration = 0;
							LastNotationNoteDuration = GetDuration(MusicalSymbols[CurrentSymbolIndex]);
						}
					}
				}
		}

		#region Constructors

		public MusicNotationFollower()
		{
		}

		public MusicNotationFollower(UpdateMuscialSymbolDelegate FollowMuscialSymbolDelegate,
			List<MusicalSymbol> MusicalSymbols)
		{
			this.FollowMuscialSymbol = FollowMuscialSymbolDelegate;
			this.MusicalSymbols = MusicalSymbols;

			minSymbolDuration = MusicSymbolDuration.Sixteenth;
			maxSymbolDuration = MusicSymbolDuration.Half;
			dottedMusicSymbolDuration = true;
			tempo = 160;
			timeSignature = new TimeSignature(4, 4);

			CalculDurationSymbols();

			CurrentSymbolIndex = FindNextMusicalIndex(0);
			if (MusicalSymbols != null)
			{
				FollowMuscialSymbol(MusicalSymbols[CurrentSymbolIndex]);
				LastNotationNoteDuration = GetDuration(MusicalSymbols[CurrentSymbolIndex]);
			}
		}

		#endregion
	}
}
