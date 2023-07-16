using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	public class Settings
	{
		public double EnemyMoveSpeed { get; set; }
		public double EnemyShotInterval { get; set; }
		public double EnemyStrategyInterval { get; set; }
		public double EnemyShotSpeed { get; set; }
		public int EnemyHealth { get; set; }
		public double EnemyInvincibilityTimer { get; set; }
		public int BasicInvaderPrice { get; set; }
		public double BasicInvaderSpeed { get; set; }
		public double BasicInvaderShotInterval { get; set; }
		public int BarrierInvaderPrice { get; set; }
		public double BarrierInvaderSpeed { get; set; }
		public double BarrierInvaderShotInterval { get; set; }
		public int BombInvaderPrice { get; set; }
		public double BombInvaderSpeed { get; set; }
		public double BombInvaderShotInterval { get; set; }
		public int FastInvaderPrice { get; set; }
		public double FastInvaderSpeed { get; set; }
		public double FastInvaderShotInterval { get; set; }
		public double InvaderShotSpeed { get; set; }
		public double BombSpeed { get; set; }
		public double BombAmplitude { get; set; }
		public double BombPeriod { get; set; }
		public int MoneyToSpend { get; set; }
		public int BarrierBlockSize { get; set; }
		public int PointsFromBarrierHit { get; set; }
		public int PointsFromEnemyKill { get; set; }
	}

}
