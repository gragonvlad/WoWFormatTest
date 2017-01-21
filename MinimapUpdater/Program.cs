﻿using CASCExplorer;
using Hjg.Pngcs;
using System;
using System.Collections.Generic;
using System.Drawing;
using WoWFormatLib.SereniaBLPLib;
using WoWFormatLib.Utils;

namespace MinimapUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            CASC.InitCasc(null, "S:\\World of Warcraft Public Test", "wowt");

            Console.WriteLine("CASC initialized for build " + CASC.cascHandler.Config.ActiveBuild);

            /*var reader = new DB6Reader(CASC.OpenFile("DBFilesClient\\Map.db2"));

            foreach (var row in reader)
            {
                var map = row.Value.GetField<string>(1);
                Console.WriteLine(map);
            }
            */

            var maps = new string[] { "Azeroth", "Kalimdor", "test", "ScottTest", "PVPZone01", "Shadowfang", "StormwindJail", "StormwindPrison", "DeadminesInstance", "PVPZone02", "Collin", "WailingCaverns", "Monastery", "RazorfenKraulInstance", "Blackfathom", "Uldaman", "GnomeragonInstance", "SunkenTemple", "RazorfenDowns", "EmeraldDream", "MonasteryInstances", "TanarisInstance", "BlackRockSpire", "BlackrockDepths", "OnyxiaLairInstance", "CavernsOfTime", "SchoolofNecromancy", "Zul'gurub", "Stratholme", "Mauradon", "DeeprunTram", "OrgrimmarInstance", "MoltenCore", "DireMaul", "AlliancePVPBarracks", "HordePVPBarracks", "development", "BlackwingLair", "PVPZone03", "AhnQiraj", "PVPZone04", "Expansion01", "AhnQirajTemple", "Karazahn", "Stratholme Raid", "HyjalPast", "HellfireMilitary", "HellfireDemon", "HellfireRampart", "HellfireRaid", "CoilfangPumping", "CoilfangMarsh", "CoilfangDraenei", "CoilfangRaid", "TempestKeepRaid", "TempestKeepArcane", "TempestKeepAtrium", "TempestKeepFactory", "AuchindounShadow", "AuchindounDemon", "AuchindounEthereal", "AuchindounDraenei", "PVPZone05", "HillsbradPast", "bladesedgearena", "BlackTemple", "GruulsLair", "NetherstormBG", "ZulAman", "Northrend", "PVPLordaeron", "ExteriorTest", "Valgarde70", "UtgardePinnacle", "Nexus70", "Nexus80", "SunwellPlateau", "Transport176244", "Transport176231", "Sunwell5ManFix", "Transport181645", "Transport177233", "Transport176310", "Transport175080", "Transport176495", "Transport164871", "Transport186238", "Transport20808", "Transport187038", "StratholmeCOT", "Transport187263", "CraigTest", "Sunwell5Man", "Ulduar70", "DrakTheronKeep", "Azjol_Uppercity", "Ulduar80", "UlduarRaid", "GunDrak", "development_nonweighted", "QA_DVD", "NorthrendBG", "DalaranPrison", "DeathKnightStart", "Transport_Tirisfal _Vengeance_Landing", "Transport_Menethil_Valgarde", "Transport_Orgrimmar_Warsong_Hold", "Transport_Stormwind_Valiance_Keep", "ChamberOfAspectsBlack", "NexusRaid", "DalaranArena", "OrgrimmarArena", "Azjol_LowerCity", "Transport_Moa'ki_Unu'pe", "Transport_Moa'ki_Kamagua", "Transport192241", "Transport192242", "WintergraspRaid", "unused", "IsleofConquest", "IcecrownCitadel", "IcecrownCitadel5Man", "AbyssalMaw", "Gilneas", "Transport_AllianceAirshipBG", "Transport_HordeAirshipBG", "AbyssalMaw_Interior", "Uldum", "BlackRockSpire_4_0", "Deephome", "Transport_Orgrimmar_to_Thunderbluff", "LostIsles", "ArgentTournamentRaid", "ArgentTournamentDungeon", "ElevatorSpawnTest", "Gilneas2", "GilneasPhase1", "GilneasPhase2", "SkywallDungeon", "QuarryofTears", "LostIslesPhase1", "Deephomeceiling", "LostIslesPhase2", "Transport197195", "HallsOfReflection", "BlackwingDescent", "GrimBatolDungeon", "GrimBatolRaid", "Transport197347", "Transport197348", "Transport197349-2", "Transport197349", "Transport197350", "Transport201834", "MountHyjalPhase1", "Firelands1", "Firelands2", "Stormwind", "ChamberofAspectsRed", "DeepholmeDungeon", "CataclysmCTF", "STV_Mine_BG", "TheBattleforGilneas", "MaelstromZone", "DesolaceBomb", "TolBarad", "AhnQirajTerrace", "TwilightHighlandsDragonmawPhase", "Transport200100", "Transport200101", "Transport200102", "Transport200103", "Transport203729", "Transport203730", "UldumPhaseOasis", "Transport 203732", "Transport203858", "Transport203859", "Transport203860", "RedgridgeOrcBomb", "RedridgeBridgePhaseOne", "RedridgeBridgePhaseTwo", "SkywallRaid", "UldumDungeon", "BaradinHold", "UldumPhasedEntrance", "TwilightHighlandsPhasedEntrance", "Gilneas_BG_2", "Transport 203861", "Transport 203862", "UldumPhaseWreckedCamp", "Transport203863", "Transport 2033864", "Transport 2033865", "Zul_Gurub5Man", "NewRaceStartZone", "FirelandsDailies", "HawaiiMainLand", "ScenarioAlcazIsland", "COTDragonblight", "COTWarOfTheAncients", "TheHourOfTwilight", "NexusLegendary", "ShadowpanHideout", "EastTemple", "StormstoutBrewery", "TheGreatWall", "DeathwingBack", "EyeoftheStorm2.0", "JadeForestAllianceHubPhase", "JadeForestBattlefieldPhase", "DarkmoonFaire", "TurtleShipPhase01", "TurtleShipPhase02", "MaelstromDeathwingFight", "TolVirArena", "MoguDungeon", "MoguInteriorRaid", "MoguExteriorRaid", "ValleyOfPower", "BFTAllianceScenario", "BFTHordeScenario", "ScarletSanctuaryArmoryAndLibrary", "ScarletMonasteryCathedralGY", "BrewmasterScenario01", "NewScholomance", "MogushanPalace", "MantidRaid", "MistsCTF3", "MantidDungeon", "MonkAreaScenario", "RuinsOfTheramore", "PandaFishingVillageScenario", "MoguRuinsScenario", "AncientMoguCryptScenario", "AncientMoguCyptDestroyedScenario", "ProvingGroundsScenario", "PetBattleJadeForest", "ValleyOfPowerScenario", "RingOfValorScenario", "BrewmasterScenario03", "BlackOxTempleScenario", "ScenarioKlaxxiIsland", "ScenarioBrewmaster04", "LevelDesignLand-DevOnly", "HordeBeachDailyArea", "AllianceBeachDailyArea", "MoguIslandDailyArea", "StormwindGunshipPandariaStartArea", "OrgrimmarGunshipPandariaStart", "TheramoreScenarioPhase", "JadeForestHordeStartingArea", "HordeAmbushScenario", "ThunderIslandRaid", "NavalBattleScenario", "DefenseOfTheAleHouseBG", "HordeBaseBeachScenario", "AllianceBaseBeachScenario", "ALittlePatienceScenario", "GoldRushBG", "JainaDalaranScenario", "WarlockArea", "BlackTempleScenario", "DarkmoonCarousel", "Draenor", "ThunderKingHordeHub", "ThunderIslandAllianceHub", "CitySiegeMoguIslandProgressionScenario", "LightningForgeMoguIslandProgressionScenario", "ShipyardMoguIslandProgressionScenario", "AllianceHubMoguIslandProgressionScenario", "HordeHubMoguIslandProgressionScenario", "FinalGateMoguIslandProgressionScenario", "MoguIslandEventsHordeBase", "MoguIslandEventsAllianceBase", "ShimmerRidgeScenario", "DarkHordeScenario", "Transport218599", "Transport218600", "ShadoPanArena", "MoguIslandLootRoom", "OrgrimmarRaid", "HeartOfTheOldGodScenario", "ProvingGrounds", "FWHordeGarrisonLevel1", "FWHordeGarrisonLevel2", "FWHordeGarrisonLevel3", "Stormgarde Keep", "HalfhillScenario", "SMVAllianceGarrisonLevel1", "SMVAllianceGarrisonLevel2", "SMVAllianceGarrisonLevel3", "CelestialChallenge", "SmallBattlegroundA", "ThePurgeOfGrommarScenario", "SmallBattlegroundB", "SmallBattlegroundC", "SmallBattlegroundD", "Transport_Siege_of_Orgrimmar_Alliance", "Transport_Siege_of_Orgrimmar_Horde", "OgreCompound", "MoonCultistHideout", "WarcraftHeroes", "PattyMackTestGarrisonBldgMap", "DraenorAuchindoun", "GORAllianceGarrisonLevel1", "GORAllianceGarrisonLevel2", "GORAllianceGarrisonLevel3", "BlastedLands", "Ashran", "Transport_Iron_Horde_Gorgrond_Train", "WarWharfSmackdown", "BonetownScenario", "FrostfireFinaleScenario", "BlackrockFoundryRaid", "TaladorIronHordeFinaleScenario", "BlackrockFoundryTrainDepot", "ArakkoaDungeon", "6HU_GARRISON_Blacksmithing_hub", "alliance_garrison_alchemy", "alliance_garrison_enchanting", "garrison_alliance_engineering", "garrison_alliance_farmhouse", "garrison_alliance_inscription", "garrison_alliance_jewelcrafting", "garrison_alliance_leatherworking", "Troll Raid", "garrison_alliance_mine_1", "garrison_alliance_mine_2", "garrison_alliance_mine_3", "garrison_alliance_stable_1", "garrison_alliance_stable_2", "garrison_alliance_stable_3", "garrison_alliance_tailoring", "HighmaulOgreRaid", "garrison_alliance_inn_1", "garrison_alliance_barn", "Transport227523", "GorHordeGarrisonLevel0", "GORHordeGarrisonLevel3", "TALAllianceGarrisonLevel0", "TALAllianceGarrisonLevel3", "TALHordeGarrisonLevel0", "TALHordeGarrisonLevel3", "SOAAllianceGarrison0", "SOAAllianceGarrison3", "SOAHordeGarrison0", "SOAHordeGarrison3", "NAGAllianceGarrisonLevel0", "NAGAllianceGarrisonLevel3", "NAGHordeGarrisonLevel0", "NAGHordeGarrisonLevel3", "garrison_alliance_armory1", "garrison_alliance_barracks1", "garrison_alliance_engineering1", "alliance_garrison_herb_garden1", "alliance_garrison_inn1", "garrison_alliance_lumbermill1", "alliance_garrison_magetower1", "garrison_alliance_pet_stable1", "garrison_alliance_salvageyard1", "garrison_alliance_storehouse1", "garrison_alliance_trading_post1", "garrison_alliance_tailoring1", "garrison_alliance_enchanting", "garrison_alliance_blacksmith1", "garrison_alliance_plot_small", "garrison_alliance_plot_medium", "garrison_alliance_plot_large", "Propland-DevOnly", "TanaanJungleIntro", "CircleofBloodScenario", "TerongorsConfrontation", "devland3", "nagrand_garrison_camp_stable_2", "DefenseOfKaraborScenario", "garrison_horde_barracks1", "ShaperDungeon", "TrollRaid2", "garrison_horde_alchemy1", "garrison_horde_armory1", "garrison_horde_barn1", "garrison_horde_blacksmith1", "garrison_horde_enchanting1", "garrison_horde_engineering1", "garrison_horde_inn1", "garrison_horde_inscription1", "garrison_horde_jewelcrafting1", "garrison_horde_leatherworking1", "garrison_horde_lumbermill1", "garrison_horde_magetower1", "garrison_horde_mine1", "garrison_alliance_petstabe", "garrison_horde_salvageyard1", "garrison_horde_sparringarena1", "garrison_horde_stable1", "garrison_horde_storehouse1", "garrison_horde_tailoring1", "garrison_horde_tradingpost1", "garrison_horde_workshop1", "garrison_alliance_workshop1", "garrison_horde_farm1", "garrison_horde_plot_large", "garrison_horde_plot_medium", "garrison_horde_plot_small", "TanaanJungleIntroForgePhase", "garrison_horde_fishing1", "garrison_alliance_fishing1", "Expansion5QAModelMap", "outdoorGarrisonArenaHorde", "outdoorGarrisonArenaAlliance", "outdoorGarrisonLumberMillAlliance", "outdoorGarrisonLumberMillHorde", "outdoorGarrisonArmoryHorde", "outdoorGarrisonArmoryAlliance", "outdoorGarrisonMageTowerHorde", "outdoorGarrisonMageTowerAlliance", "outdoorGarrisonStablesHorde", "outdoorGarrisonStablesAlliance", "outdoorGarrisonWorkshopHorde", "outdoorGarrisonWorkshopAlliance", "outdoorGarrisonInnHorde", "outdoorGarrisonInnAlliance", "outdoorGarrisonTradingPostHorde", "outdoorGarrisonTradingPostAlliance", "outdoorGarrisonConstructionPlotHorde", "outdoorGarrisonConstructionPlotAlliance", "GrommasharScenario", "FWHordeGarrisonLeve2new", "SMVAllianceGarrisonLevel2new", "garrison_horde_barracks2", "garrison_horde_armory2", "garrison_horde_barn2", "garrison_horde_inn2", "garrison_horde_lumbermill2", "garrison_horde_magetower2", "garrison_horde_petstable2", "garrison_horde_stable2", "garrison_horde_tradingpost2", "garrison_horde_workshop2", "garrison_horde_barracks3", "garrison_horde_armory3", "garrison_horde_barn3", "garrison_horde_inn3", "garrison_horde_magetower3", "garrison_horde_petstable3", "garrison_horde_stable3", "garrison_horde_tradingpost3", "garrison_horde_workshop3", "Garrison_Alliance_Large_Construction", "Garrison_Alliance_Medium_Construction", "Garrison_Horde_Large_Construction", "Garrison_Horde_Medium_Construction", "UpperBlackRockSpire", "garrisonAllianceMageTower2", "garrisonAllianceMageTower3", "garrison_horde_mine2", "garrison_horde_mine3", "garrison_alliance_workshop2", "garrison_alliance_workshop3", "garrison_alliance_lumbermill2", "garrison_alliance_lumbermill3", "Garrison_Horde_Small_Construction", "Garrison_Alliance_Small_Construction", "AuchindounQuest", "alliance_garrison_alchemy_rank2", "alliance_garrison_alchemy_rank3", "garrison_alliance_blacksmith2", "garrison_alliance_enchanting2", "garrison_alliance_engineering2", "garrison_alliance_inscription2", "garrison_alliance_inscription3", "garrison_alliance_jewelcrafting2", "garrison_alliance_jewelcrafting3", "garrison_alliance_leatherworking2", "garrison_alliance_leatherworking3", "garrison_alliance_tailoring2", "garrison_alliance_storehouse2", "garrison_alliance_storehouse3", "garrison_horde_storehouse2", "garrison_horde_storehouse3", "garrison_alliance_salvageyard2", "garrison_alliance_salvageyard3", "garrison_horde_lumbermill3", "garrison_alliance_pet_stable2", "garrison_alliance_pet_stable3", "garrison_alliance_trading_post2", "garrison_alliance_trading_post3", "garrison_alliance_barn2", "garrison_alliance_barn3", "garrison_alliance_inn_2", "garrison_alliance_inn_3", "GorgrondFinaleScenario", "garrison_alliance_barracks2", "garrison_alliance_barracks3", "garrison_alliance_armory2", "garrison_alliance_armory3", "GorgrondFinaleScenarioMap", "garrison_horde_sparringarena2", "garrison_horde_sparringarena3", "garrison_horde_alchemy2", "garrison_horde_alchemy3", "garrison_horde_blacksmith2", "garrison_horde_blacksmith3", "garrison_horde_enchanting2", "garrison_horde_enchanting3", "garrison_horde_inscription2", "garrison_horde_inscription3", "garrison_horde_leatherworking2", "garrison_horde_leatherworking3", "garrison_horde_jewelcrafting2", "garrison_horde_jewelcrafting3", "garrison_horde_tailoring2", "garrison_horde_tailoring3", "garrison_horde_salvageyard2", "garrison_horde_salvageyard3", "PattyMackTestGarrisonBldgMap2", "garrison_horde_engineering2", "garrison_horde_engineering3", "SparringArenaLevel3Stadium", "garrison_horde_fishing2", "garrison_horde_fishing3", "garrison_alliance_fishing2", "garrison_alliance_fishing3", "garrison_alliance_petstable1", "garrison_alliance_petstable2", "garrison_alliance_petstable3", "garrison_alliance_infirmary1", "garrison_alliance_infirmary2", "garrison_alliance_infirmary3", "outdoorGarrisonConstructionPlotAllianceLarge", "outdoorGarrisonConstructionPlotHordeLarge", "HellfireRaid62", "TanaanLegionTest", "ScourgeofNorthshire", "ArtifactAshbringerOrigin", "EdgeofRealityMount", "NagaDungeon", "FXlDesignLand-DevOnly", "7_DungeonExteriorNeltharionsLair", "Transport_The_Iron_Mountain", "BrokenShoreScenario", "AzsunaScenario", "IllidansRock", "HelhiemExteriorArea", "TanaanJungle", "TanaanJungleNoHubsPhase", "Emerald_Nightmare_ValSharah_exterior", "WardenPrison", "MaelstromShaman", "Legion Dungeon", "1466", "GarrisonAllianceShipyard", "GarrisonHordeShipyard", "TheMawofNashal", "Transport_The_Maw_of_Nashal", "Valhallas", "ValSharahTempleofEluneScenario", "WarriorArtifactArea", "DeathKnightArtifactArea", "legionnexus", "GarrisonShipyardAllianceSubmarine", "GarrisonShipyardAllianceDestroyer", "GarrisonShipyardTransport", "GarrisonShipyardDreadnaught", "GarrisonShipyardCarrier", "GarrisonShipyardHordeSubmarine", "GarrisonShipyardHordeDestroyer", "Artifact-PortalWorldAcqusition", "Helheim", "WardenPrisonDungeon", "AcquisitionVioletHold", "AcquisitionWarriorProt", "GarrisonShipyardCarrierAlliance", "GarrisonShipyardGalleonHorde", "AcquisitionHavoc", "Artifact-Warrior Fury Acquisition", "ArtifactPaladinRetAcquisition", "BlackRookHoldDungeon", "DalaranUnderbelly", "ArtifactShamanElementalAcquisition", "BlackrookHoldArena", "NagrandArena2", "BloodtotemCavernFelPhase", "BloodtotemCavernTaurenPhase", "Artifact-WarriorFuryAcquisition", "Artifact-PriestHunterOrderHall", "Artifact-MageOrderHall", "Artifact-MonkOrderHall", "HulnHighmountain", "SuramarCatacombsDungeon", "StormheimPrescenarioWindrunner", "StormheimPrescenarioSkyfire", "ArtifactsDemonHunterOrderHall", "NightmareRaid", "ArtifactWarlockOrderHallScenario", "MardumScenario", "Artifact-WhiteTigerTempleAcquisition", "HighMountain", "Artifact-SkywallAcquisition", "KarazhanScenario", "SuramarRaid", "HighMountainMesa", "Artifact-KarazhanAcquisition", "Artifact-DefenseofMoongladeScenario", "DefenseofMoongladeScenario", "UrsocsLairScenario", "BoostExperience", "Karazhan Scenario", "Artifact-AcquisitionArmsHolyShadow", "Artifact-Dreamway", "Artifact-TerraceofEndlessSpringAcquisition", "LegionVioletHoldDungeon", "Artifact-Acquisition-CombatResto", "Artifacts-CombatAcquisitionShip", "TechTestSeamlessWorldTransitionA", "TechTestSeamlessWorldTransitionB", "ValsharahArena", "Artifact-Acquisition-Underlight", "BoostExperience2", "TransportBoostExperienceAllianceGunship", "TransportBoostExperienceHordeGunship", "BoostExperience2Horde", "TransportBoostExperienceHordeGunship2", "TransportBoostExperienceAllianceGunship2", "TechTestCosmeticParentPerformance", "SuramarCityDungeon", "MaelstromShamanHubIntroScenario", "UdluarScenario", "MaelstromTitanScenario", "Artifactï¿½DalaranVaultAcquisition", "Artifact-DalaranVaultAcquisition", "JulienTestLand-DevOnly", "AssualtOnStormwind", "DevMapA", "DevMapB", "DevMapC", "DevMapD", "DevMapE", "DevMapF", "DevMapG", "ArtifactRestoAcqusition", "ArtifactThroneoftheTides", "SkywallDungeon_OrderHall", "AbyssalMaw_Interior_Scenario", "Artifact-PortalWorldNaskora", "FirelandsArtifact", "ArtifactAcquisitionSubtlety", "Hyjal Instance", "AcquisitionTempleofstorms", "Artifact-SerenityLegionScenario", "DeathKnightCampaign-LightsHopeChapel", "TheRuinsofFalanaar", "Faronaar", "DeathKnightCampaign-Undercity", "DeathKnightCampaign-ScarletMonastery", "ArtifactStormwind", "BlackTemple-Legion", "IllidanTemp", "MageCampaign-TheOculus", "BattleofExodar", "TrialoftheSerpent", "TheCollapseSuramarScenario", "FelHammerDHScenario", "Transport251513", "NetherlightTemplePrison", "TolBarad1", "TheArcwaySuramarScenario", "TransportAllianceShipPhaseableMO", "TransportHordeShipPhaseableMO", "TransportKvaldirShipPhaseableMO", "BlackRookSenario", "VoljinsFuneralPyre", "Helhiem2", "Transport254124", "Acherus", "Karazahn1", "LightsHeart", "BladesEdgeArena2", "EnvironmentLandDevOnly", "SuramarEndScenario", "DungeonBlockout", "BrokenShoreIntro", "LegionShipVertical", "LegionShipHorizontal", "BrokenshorePristine", "BrokenShorePrepatch", "bladesedgearena2b", "EyeofEternityScenario", "TombofSargerasRaid", "TombofSargerasDeungeon", "ABWinter", "ArtifactsDemonHunterOrderHallPhase", "ArtifactGnomeregan", "dreadscarriftwarlockplatform", "WailingCavernsPetBattle", "DeadminesPetBattle", "EyeofEternityMageClassMount", "CookingImpossible", "PitofSaronDeathKnight", "MardumScenarioClientScene", "GnomereganPetBattle", "BrokenShoreBattleshipFinale", "LegionCommandCenter", "LegionSpiderCave", "ArtifactAcquisitionTank", "LegionFelCave", "LegionFelFirenovaArea", "LegionBarracks", "ArtifactHighmountainDualBoss", "HallsofValorScenario", "LegionShipHorizontalValsharah", "LegionShipHorizontalAzsuna", "LegionShipHorizontalHighMountain", "LegionShipHorizontalStormheim", "StratholmePaladinClassMount", "BlackRookHoldArtifactChallenge", "SouthseaPirateShip715BoatHoliday", "hearthstonetavern", "HallsOfValorWarriorClassMount", "BlackrockMountainBrawl", "brokenshorewardentower", "warlockmountscenario", "ColdridgeValley", "HallsofValorHunterScenario", "EyeofEternityMageClassMountShort", "ShrineofAvianaDefenseScenario", "DruidMountFinaleScenario", "FelwingLedgeDemonHunterClassMount", "ThroneoftheFourWindsShamanClassMounts", "DKMountScenario", "RubySanctumDKMountScenario", "AkazamarakHatScenario", "LostGlacierDKMountScenario" };
            //var maps = new string[] { "ABWinter" };
            foreach(var map in maps)
            {
                var mapname = map;

                var hasMinimaps = false;

                var min_x = 64;
                var min_y = 64;

                var max_x = 0;
                var max_y = 0;

                Console.WriteLine("[" + mapname + "] Loading tiles..");

                var bmpDict = new Dictionary<string, Bitmap>();

                for (int cur_x = 0; cur_x < 64; cur_x++)
                {
                    for (int cur_y = 0; cur_y < 64; cur_y++)
                    {
                        var tilename = "World\\Minimaps\\" + mapname + "\\map" + cur_x + "_" + cur_y + ".blp";

                        if (CASC.FileExists(tilename))
                        {
                            hasMinimaps = true;

                            if (cur_x > max_x) { max_x = cur_x; }
                            if (cur_y > max_y) { max_y = cur_y; }

                            if (cur_x < min_x) { min_x = cur_x; }
                            if (cur_y < min_y) { min_y = cur_y; }

                            using (var blp = new BlpFile(CASC.OpenFile(tilename)))
                            {
                                bmpDict.Add(cur_x + "_" + cur_y, blp.GetBitmap(0));
                            }
                        }
                    }
                }

                if(hasMinimaps == false)
                {
                    Console.WriteLine("[" + mapname + "] " + "Skipping map, has no minimap tiles");
                    continue;
                }

                Console.WriteLine("[" + mapname + "] MIN: (" + min_x + " " + min_y + ") MAX: (" + max_x + " " + max_y + ")");

                var res_x = (((max_x - min_x) * 256) + 256);
                var res_y = (((max_y - min_y) * 256) + 256);

                if (res_x < 0 || res_y < 0)
                {
                    Console.WriteLine("Invalid resolution!");
                }

                Console.WriteLine("[" + mapname + "] " + "Image will be " + res_x + "x" + res_y);


                ImageInfo imi = new ImageInfo(res_x, res_y, 8, true);
                PngWriter png = FileHelper.CreatePngWriter("maps/" + mapname + ".png", imi, true);

                for (int row = 0; row < png.ImgInfo.Rows; row++)
                {
                    var blp_y = min_y + (row / 256);
                    var blp_pixel_y = row - ((blp_y - min_y) * 256);

                    ImageLine iline = new ImageLine(imi);
                    for (int col = 0; col < imi.Cols; col++)
                    {
                        var blp_x = min_x + (col / 256);
                        var blp_pixel_x = col - ((blp_x - min_x) * 256);

                        if(bmpDict.ContainsKey(blp_x + "_" + blp_y))
                        {
                            var pixel = bmpDict[blp_x + "_" + blp_y].GetPixel(blp_pixel_x, blp_pixel_y);
                            ImageLineHelper.SetPixel(iline, col, pixel.R, pixel.G, pixel.B, pixel.A);
                        }
                        else
                        {
                            ImageLineHelper.SetPixel(iline, col, 0x00, 0x00, 0x00, 0x00);
                        }
                    }
                    png.WriteRow(iline, row);
                    if(row % (png.ImgInfo.Rows / 100) == 0)
                    {
                        Console.Write("\r[" + mapname + "] Writing image: " + Math.Round((double)(100 * row) / png.ImgInfo.Rows) + "%");
                    }
                }

                png.End();
                GC.Collect();

                Console.WriteLine("\n[" + mapname + "] Done");
            }
            Console.ReadLine();
        }
    }
}
