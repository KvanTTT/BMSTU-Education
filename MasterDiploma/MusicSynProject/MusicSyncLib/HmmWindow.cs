namespace MusicSyncLib
{
	public struct HmmWindow
	{
		public int LeftBound;
		public int RightBound;

		public HmmWindow(int windowSize, int currentPos, int maxStatesCount)
		{
			int semiWindowSize = windowSize / 2;
			int startPos = currentPos;
			if (currentPos - semiWindowSize < 0)
				startPos = semiWindowSize;
			else if (startPos + (windowSize + 1) / 2 > maxStatesCount)
				startPos = maxStatesCount - (windowSize + 1) / 2;
			LeftBound = startPos - semiWindowSize;
			RightBound = startPos + (windowSize + 1) / 2;
		}
	}
}
