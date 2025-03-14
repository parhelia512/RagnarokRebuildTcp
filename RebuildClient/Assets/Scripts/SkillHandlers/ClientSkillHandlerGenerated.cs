using Assets.Scripts.SkillHandlers.Handlers;

namespace Assets.Scripts.SkillHandlers
{
	public static partial class ClientSkillHandler
	{
		static ClientSkillHandler()
		{
			handlers = new SkillHandlerBase[133];
			handlers[0] = new DefaultSkillHandler();
			handlers[1] = new DefaultSkillHandler();
			handlers[2] = new FirstAidHandler();
			handlers[3] = new BashHandler();
			handlers[4] = new EndureSkillHandler();
			handlers[5] = new DefaultSkillHandler();
			handlers[6] = new DefaultSkillHandler();
			handlers[7] = new ProvokeHandler();
			handlers[8] = new DefaultSkillHandler();
			handlers[9] = new DefaultSkillHandler();
			handlers[10] = new DefaultSkillHandler();
			handlers[11] = new FireBoltHandler();
			handlers[11].ExecuteWithoutSource = true;
			handlers[12] = new ColdBoltHandler();
			handlers[12].ExecuteWithoutSource = true;
			handlers[13] = new FireBallHandler();
			handlers[14] = new FireWallHandler();
			handlers[14].ExecuteWithoutSource = true;
			handlers[15] = new FrostDiverHandler();
			handlers[16] = new LightningBoltHandler();
			handlers[16].ExecuteWithoutSource = true;
			handlers[17] = new NapalmBeatHandler();
			handlers[18] = new SoulStrikeHandler();
			handlers[19] = new ThunderStormHandler();
			handlers[20] = new SafetyWallHandler();
			handlers[21] = new StoneCurseHandler();
			handlers[22] = new SightHandler();
			handlers[23] = new DefaultSkillHandler();
			handlers[24] = new DoubleStrafeHandler();
			handlers[25] = new ChargeArrowHandler();
			handlers[26] = new ArrowShowerHandler();
			handlers[27] = new ImproveConcentrationHandler();
			handlers[28] = new DefaultSkillHandler();
			handlers[29] = new DefaultSkillHandler();
			handlers[30] = new HealHandler();
			handlers[30].ExecuteWithoutSource = true;
			handlers[31] = new IncreaseAgiHandler();
			handlers[32] = new DefaultSkillHandler();
			handlers[33] = new BlessingHandler();
			handlers[33].ExecuteWithoutSource = true;
			handlers[34] = new DefaultSkillHandler();
			handlers[35] = new DefaultSkillHandler();
			handlers[36] = new AngelusHandler();
			handlers[37] = new DefaultSkillHandler();
			handlers[38] = new DefaultSkillHandler();
			handlers[39] = new DefaultSkillHandler();
			handlers[40] = new PneumaHandler();
			handlers[40].ExecuteWithoutSource = true;
			handlers[41] = new RuwachHandler();
			handlers[42] = new DefaultSkillHandler();
			handlers[43] = new WarpPortalHandler();
			handlers[44] = new DefaultSkillHandler();
			handlers[45] = new DefaultSkillHandler();
			handlers[46] = new EnvenomHandler();
			handlers[47] = new DefaultSkillHandler();
			handlers[48] = new BackslideHandler();
			handlers[49] = new StealHandler();
			handlers[50] = new DefaultSkillHandler();
			handlers[51] = new DefaultSkillHandler();
			handlers[52] = new DefaultSkillHandler();
			handlers[53] = new HidingHandler();
			handlers[54] = new DefaultSkillHandler();
			handlers[55] = new DefaultSkillHandler();
			handlers[56] = new DefaultSkillHandler();
			handlers[57] = new DefaultSkillHandler();
			handlers[58] = new DefaultSkillHandler();
			handlers[59] = new DefaultSkillHandler();
			handlers[60] = new MammoniteHandler();
			handlers[61] = new DefaultSkillHandler();
			handlers[62] = new CrazyUproarHandler();
			handlers[63] = new CartRevolutionHandler();
			handlers[64] = new TwoHandQuickenHandler();
			handlers[65] = new ResurrectionHandler();
			handlers[66] = new HammerFallHandler();
			handlers[67] = new SonicBlowHandler();
			handlers[68] = new DefaultSkillHandler();
			handlers[69] = new DefaultSkillHandler();
			handlers[70] = new DefaultSkillHandler();
			handlers[71] = new LordOfVermilionHandler();
			handlers[72] = new DefaultSkillHandler();
			handlers[73] = new DefaultSkillHandler();
			handlers[74] = new FireAttackHandler();
			handlers[75] = new IceAttackHandler();
			handlers[76] = new WaterAttackHandler();
			handlers[77] = new WindAttackHandler();
			handlers[78] = new EarthAttackHandler();
			handlers[79] = new DarkAttackHandler();
			handlers[80] = new HolyAttackHandler();
			handlers[81] = new PoisonAttackHandler();
			handlers[82] = new UndeadAttackHandler();
			handlers[83] = new GhostAttackHandler();
			handlers[84] = new CriticalSlashHandler();
			handlers[84].ExecuteWithoutSource = true;
			handlers[85] = new DefaultSkillHandler();
			handlers[86] = new PoisonHandler();
			handlers[87] = new SleepAttackHandler();
			handlers[88] = new StunAttackHandler();
			handlers[88].ExecuteWithoutSource = true;
			handlers[89] = new DefaultSkillHandler();
			handlers[90] = new SilenceAttackHandler();
			handlers[91] = new DefaultSkillHandler();
			handlers[92] = new DefaultSkillHandler();
			handlers[93] = new DefaultSkillHandler();
			handlers[94] = new DefaultSkillHandler();
			handlers[95] = new DefaultSkillHandler();
			handlers[96] = new DefaultSkillHandler();
			handlers[97] = new DefaultSkillHandler();
			handlers[98] = new DefaultSkillHandler();
			handlers[99] = new DefaultSkillHandler();
			handlers[100] = new DefaultSkillHandler();
			handlers[101] = new SelfDestructHandler();
			handlers[101].ExecuteWithoutSource = true;
			handlers[102] = new PowerUpHandler();
			handlers[103] = new SpeedUpHandler();
			handlers[104] = new CallMinionHandler();
			handlers[105] = new DefaultSkillHandler();
			handlers[106] = new DefaultSkillHandler();
			handlers[107] = new DefaultSkillHandler();
			handlers[108] = new BloodDrainHandler();
			handlers[109] = new EnergyDrainHandler();
			handlers[110] = new GuidedAttackHandler();
			handlers[111] = new DefaultSkillHandler();
			handlers[112] = new ComboAttackHandler();
			handlers[113] = new DefaultSkillHandler();
			handlers[114] = new DarkStrikeHandler();
			handlers[115] = new DefaultSkillHandler();
			handlers[116] = new DefaultSkillHandler();
			handlers[117] = new DefaultSkillHandler();
			handlers[118] = new DefaultSkillHandler();
			handlers[119] = new SmokingHandler();
			handlers[120] = new DefaultSkillHandler();
			handlers[121] = new DefaultSkillHandler();
			handlers[122] = new DefaultSkillHandler();
			handlers[123] = new DefaultSkillHandler();
			handlers[124] = new DefaultSkillHandler();
			handlers[125] = new BulwarkHandler();
			handlers[126] = new DefaultSkillHandler();
			handlers[127] = new GrandThunderstormHandler();
			handlers[128] = new DefaultSkillHandler();
			handlers[129] = new DefaultSkillHandler();
			handlers[130] = new DefaultSkillHandler();
			handlers[131] = new DefaultSkillHandler();
			handlers[132] = new DefaultSkillHandler();
		}
	}
}
