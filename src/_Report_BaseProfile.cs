namespace Exemplar.Api.MappingProfiles
{
    using System.Linq;
    using AutoMapper;
    using Exemplar.Api.Extensions;
    using Exemplar.Domain;
    using Exemplar.Dto;
    using Exemplar.Dto.PropertyInspectionForm;


    // <>LIST OF DATA FIELDS TO BE UPDATED<>
    // Addition of Questions:

    // Shingle Width: (Added to Roof)
    // Shingle Exposure: (Added to Roof)

    // HvacOther (Added to Roof Damaged Items)
    // OffRidgeVent Material (Added to Roof Damaged Items)
    // DryerExhaust Present? (Added to Roof Damaged Items)
    // SwampCooler Present? (Added to Roof Damaged Items)
    // SolarPanel Present? (Added to Roof Damaged Items)
    // FlatRoofScupperDrain Present? (Added to Roof Damaged Items)

    // Gutter Questions Added/Removed (Added to Roof)

    // Moving of Questions (will have to move properties between the models of sections):
    // Shingle Gauge Photo Taken? (needs to be moved to roof but resides in main until)
    // Pitch Gauge Photo Taken? (needs to be moved to roof but resides in main until)

    // Fascia (needs to be moved to roof but resides in elevation until)
    // Drip Edge Eaves Present? (needs to be moved to roof but resides in roof damaged items until)
    // Drip Edge Rake Present? (needs to be moved to roof but resides in roof damaged items until)
    // Valley Metal Present? (needs to be moved to roof but resides in roof damaged items until)

    // Questions between Was a copy of Eagleview Provided? and Tarp Install / Remove & Reset (SF):
    // (needs to be moved to main but reside in roof - additional items until)

    // Removal of Questions:
    // Fascia N,S,E,W Damaged (needs to be removed in the migrations)

    public class Report_BaseReportProfile : Profile
    {
        public Report_BaseReportProfile()
        {
            CreateMap<PropertyInspectionForm, BaseReportDto>()
                // TODO: Add AllPhotosPitch and AllPhotosShingle to PropertyInspectionFormRoof
                // TODO: Move DripEdgeEave and DripEdgeRake to PropertyInspectionFormRoof from PropertyInspectionFormRoofDamagedItem
                // TODO: Move ValleyMetal to PropertyInspectionFormRoof from PropertyInspectionFormRoofDamagedItem
                // TODO: Add FreeForm to PropertyInspectionFormRoof
                .ForMember(dest => dest.WindowWrapDamaged, opt => opt.MapFrom( src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.WindowWrapDamaged) ))
                .ForMember(dest => dest.WindowWrapDamageAmt, opt => opt.MapFrom( src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.WindowWrapLf) ))
                .ForMember(dest => dest.WindowWrapPainted, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.WindowWrapPainted)))
                .ForMember(dest => dest.WindowWrapMaterial, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.WindowWrapMaterial, src.PropertyInspectionFormElevation.WindowWrapMaterialOther)))
                .ForMember(dest => dest.WindowDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowsDamaged.Length > 0 ? src.PropertyInspectionFormElevation.WindowsDamaged : string.Empty))
                .ForMember(dest => dest.WindowSmallSf, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowSmallSf.HasValue ? src.PropertyInspectionFormElevation.WindowSmallSf.ToString() : string.Empty))
                .ForMember(dest => dest.WindowMediumSf, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowMediumSf.HasValue ? src.PropertyInspectionFormElevation.WindowMediumSf.ToString() : string.Empty))
                .ForMember(dest => dest.WindowLargeSf, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowLargeSf.HasValue ? src.PropertyInspectionFormElevation.WindowLargeSf.ToString() : string.Empty))
                .ForMember(dest => dest.WindowXlargeSf, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowXlargeSf.HasValue ? src.PropertyInspectionFormElevation.WindowXlargeSf.ToString() : string.Empty))
                .ForMember(dest => dest.WindowOtherSf, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowOtherSf.HasValue ? src.PropertyInspectionFormElevation.WindowOtherSf.ToString() : string.Empty))
                .ForMember(dest => dest.WindowOtherDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowOtherDescription.Length > 0 ? src.PropertyInspectionFormElevation.WindowOtherDescription : string.Empty))
                .ForMember(dest => dest.OtherConditionsDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.OtherConditions.HasValue && src.PropertyInspectionFormRoof.OtherConditions.Value && src.PropertyInspectionFormRoof.OtherConditionsDescription.Length > 0 ? "Other Conditions (" + src.PropertyInspectionFormRoof.OtherConditionsDescription + ")|" : string.Empty))
                .ForMember(dest => dest.StormDamageTypeOther, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.StormDamageTypeOther.Length > 0 ? src.PropertyInspectionFormRoof.StormDamageTypeOther : string.Empty))
                .ForMember(dest => dest.StormDamageType, opt => opt.MapFrom( src => src.PropertyInspectionFormRoof.StormDamageType.Contains("Other") ? "Other" : "No" ))
                .ForMember(dest => dest.PriorRepairsFront, opt => opt.MapFrom(
                    src => src.PropertyInspectionFormMain.DirectionNorth == "Front"
                      ? src.PropertyInspectionFormRoof.PriorRepairsNorth.Length > 0
                          ? src.PropertyInspectionFormRoof.PriorRepairsNorth
                          : string.Empty
                      : src.PropertyInspectionFormMain.DirectionNorth == "Back"
                      ? src.PropertyInspectionFormRoof.PriorRepairsSouth.Length > 0
                          ? src.PropertyInspectionFormRoof.PriorRepairsSouth
                          : string.Empty
                      : src.PropertyInspectionFormMain.DirectionNorth == "Left"
                      ? src.PropertyInspectionFormRoof.PriorRepairsWest.Length > 0
                          ? src.PropertyInspectionFormRoof.PriorRepairsWest
                          : string.Empty
                      : src.PropertyInspectionFormMain.DirectionNorth == "Right"
                      ? src.PropertyInspectionFormRoof.PriorRepairsEast.Length > 0
                          ? src.PropertyInspectionFormRoof.PriorRepairsEast
                          : string.Empty
                      : "NA"))
                .ForMember(dest => dest.PriorRepairsBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.PriorRepairsSouth.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsSouth : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.PriorRepairsNorth.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsNorth : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.PriorRepairsEast.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsEast : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.PriorRepairsWest.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsWest : string.Empty
                    : "NA"))
                .ForMember(dest => dest.PriorRepairsLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.PriorRepairsEast.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsEast : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.PriorRepairsWest.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsWest : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.PriorRepairsNorth.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsNorth : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.PriorRepairsSouth.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsSouth : string.Empty
                    : "NA"))
                .ForMember(dest => dest.PriorRepairsRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.PriorRepairsWest.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsWest : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.PriorRepairsEast.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsEast : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.PriorRepairsSouth.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsSouth : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.PriorRepairsNorth.Length > 0
                    ? src.PropertyInspectionFormRoof.PriorRepairsNorth : string.Empty
                    : "NA")
                .ForMember(dest => dest.BrittlenessTestPassFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass : string.Empty
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestPassLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass : string.Empty
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestPassRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass : string.Empty
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestPassBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestSouthPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestNorthPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestEastPass : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass.Length > 0 ? src.PropertyInspectionFormRoof.BrittlenessTestWestPass : string.Empty
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestWest.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestWest.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestEast.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestEast.Value ? "Yes" : "No" : "NA"
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestEast.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestEast.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestWest.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestWest.Value ? "Yes" : "No" : "NA"
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestEast.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestEast.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestWest.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestWest.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.Value ? "Yes" : "No" : "NA"
                    : "NA"))
                .ForMember(dest => dest.BrittlenessTestRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.BrittlenessTestWest.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestWest.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.BrittlenessTestEast.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestEast.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestSouth.Value ? "Yes" : "No" : "NA"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.HasValue ? src.PropertyInspectionFormRoof.BrittlenessTestNorth.Value ? "Yes" : "No" : "NA"
                    : "NA"))
                .ForMember(dest => dest.WindDamageBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                    ((src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                    ((src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                    ((src.PropertyInspectionFormRoof.WindDamageEastFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                    ((src.PropertyInspectionFormRoof.WindDamageWestFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestOneSqft : 0)).ToString()
                    : "NA"))
                .ForMember(dest => dest.WindDamageFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                    ((src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                    ((src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                    ((src.PropertyInspectionFormRoof.WindDamageWestFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                    ((src.PropertyInspectionFormRoof.WindDamageEastFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastOneSqft : 0)).ToString()
                    : "NA"))
                .ForMember(dest => dest.WindDamageLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                    ((src.PropertyInspectionFormRoof.WindDamageEastFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                    ((src.PropertyInspectionFormRoof.WindDamageWestFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                    ((src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                    ((src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthOneSqft : 0)).ToString()
                    : "NA"))
                .ForMember(dest => dest.WindDamageRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                    ((src.PropertyInspectionFormRoof.WindDamageWestFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageWestOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageWestOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                    ((src.PropertyInspectionFormRoof.WindDamageEastFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageEastOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageEastOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                    ((src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageSouthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageSouthOneSqft : 0)).ToString()
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                    ((src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFiveSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthFourSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthFourSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthThreeSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthTwoSqft : 0)
                    + (src.PropertyInspectionFormRoof.WindDamageNorthOneSqft.HasValue ? src.PropertyInspectionFormRoof.WindDamageNorthOneSqft : 0)).ToString()
                    : "NA"))
                // Wind Damage Direction
                .ForMember(dest => dest.DamageDirectionBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? "South"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? "North"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? "East"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? "West" : string.Empty))
                .ForMember(dest => dest.DamageDirectionFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? "North"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? "South"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? "West"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? "East" : string.Empty))
                .ForMember(dest => dest.DamageDirectionLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? "East"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? "West"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? "North"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? "South" : string.Empty))
                .ForMember(dest => dest.DamageDirectionRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? "West"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? "East"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? "South"
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? "North" : string.Empty))
                .ForMember(dest => dest.HailDamageBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.HailDamageSouthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageSouthSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.HailDamageNorthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageNorthSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.HailDamageEastSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageEastSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.HailDamageWestSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageWestSf.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.HailDamageFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.HailDamageNorthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageNorthSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.HailDamageSouthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageSouthSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.HailDamageWestSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageWestSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.HailDamageEastSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageEastSf.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.HailDamageLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.HailDamageEastSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageEastSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.HailDamageWestSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageWestSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.HailDamageNorthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageNorthSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.HailDamageSouthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageSouthSf.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.HailDamageRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.HailDamageWestSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageWestSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.HailDamageEastSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageEastSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.HailDamageSouthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageSouthSf.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.HailDamageNorthSf.HasValue ? src.PropertyInspectionFormRoof.HailDamageNorthSf.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.OtherDamageBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.OtherDamageSouth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageSouth.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.OtherDamageNorth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageNorth.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.OtherDamageEast.HasValue ? src.PropertyInspectionFormRoof.OtherDamageEast.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.OtherDamageWest.HasValue ? src.PropertyInspectionFormRoof.OtherDamageWest.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.OtherDamageFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.OtherDamageNorth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageNorth.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.OtherDamageSouth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageSouth.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.OtherDamageWest.HasValue ? src.PropertyInspectionFormRoof.OtherDamageWest.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.OtherDamageEast.HasValue ? src.PropertyInspectionFormRoof.OtherDamageEast.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.OtherDamageLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.OtherDamageEast.HasValue ? src.PropertyInspectionFormRoof.OtherDamageEast.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.OtherDamageWest.HasValue ? src.PropertyInspectionFormRoof.OtherDamageWest.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.OtherDamageNorth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageNorth.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.OtherDamageSouth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageSouth.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.OtherDamageRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.OtherDamageWest.HasValue ? src.PropertyInspectionFormRoof.OtherDamageWest.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.OtherDamageEast.HasValue ? src.PropertyInspectionFormRoof.OtherDamageEast.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.OtherDamageSouth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageSouth.ToString() : string.Empty
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.OtherDamageNorth.HasValue ? src.PropertyInspectionFormRoof.OtherDamageNorth.ToString() : string.Empty
                    : "NA"))
                .ForMember(dest => dest.AdditionalSummary, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.AdditionalSummary))
                // Collateral Items
                .ForMember(dest => dest.AwningDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.AwningDamaged))
                .ForMember(dest => dest.AwningMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.AwningMaterial == "Other" ? src.PropertyInspectionFormElevation.AwningMaterialOther : src.PropertyInspectionFormElevation.AwningMaterial))
                .ForMember(dest => dest.CoilsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.RefrigerationCoilsDamaged))
                .ForMember(dest => dest.CoilsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.RefrigerationCoilsDamaged == "Yes" ? src.PropertyInspectionFormElevation.RefrigerationCoilUnits.ToString() : string.Empty))
                .ForMember(dest => dest.DeckDamaged, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DeckDamaged)))
                .ForMember(dest => dest.DeckMaterial, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DeckMaterial)))
                .ForMember(dest => dest.DeckPainted, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DeckPainted)))
                .ForMember(dest => dest.DeckDamageAmt, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DeckSf)))
                .ForMember(dest => dest.DeckStained, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DeckStained)))
                .ForMember(dest => dest.FasciaDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.FasciaDamaged.Length > 0 ? src.PropertyInspectionFormElevation.FasciaDamaged : string.Empty))
                //.ForMember(dest => dest.FasciaDamageAmt, opt => opt.MapFrom(src =>
                //    src.PropertyInspectionFormElevation.FasciaDamaged.Length > 0 && src.PropertyInspectionFormElevation.FasciaDamaged == "Yes" ?
                //    (src.PropertyInspectionFormElevation.FasciaNorthDamaged == "Yes" ? $"N ({src.PropertyInspectionFormElevation.FasciaNorthLf.ToString()} LF) " : string.Empty) +
                //    (src.PropertyInspectionFormElevation.FasciaSouthDamaged == "Yes" ? $" S ({src.PropertyInspectionFormElevation.FasciaSouthLf.ToString()} LF) " : string.Empty) +
                //    (src.PropertyInspectionFormElevation.FasciaNorthDamaged == "Yes" ? $" E ({src.PropertyInspectionFormElevation.FasciaEastLf.ToString()} LF) " : string.Empty) +
                //    (src.PropertyInspectionFormElevation.FasciaSouthDamaged == "Yes" ? $" W ({src.PropertyInspectionFormElevation.FasciaWestLf.ToString()} LF) " : string.Empty) : string.Empty))
                .ForMember(dest => dest.FasciaMaterial, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.FasciaMaterial, src.PropertyInspectionFormRoof.FasciaMaterialOther)))
                .ForMember(dest => dest.FasciaSize, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.FasciaSize, src.PropertyInspectionFormRoof.FasciaSizeOther)))
                .ForMember(dest => dest.FasciaDamaged, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.FasciaDamaged)))
                .ForMember(dest => dest.FenceDamaged, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.FenceDamaged)))
                .ForMember(dest => dest.FenceSize, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.FenceHeightLf)))
                .ForMember(dest => dest.FenceDamageAmt, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.FenceSf)))
                .ForMember(dest => dest.FenceMaterial, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.FenceMaterial, src.PropertyInspectionFormElevation.FenceMaterialOther)))
                .ForMember(dest => dest.FencePainted, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.FencePainted)))
                .ForMember(dest => dest.FenceStained, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.FenceStained)))
                .ForMember(dest => dest.DownspoutsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.DownSpoutsDamaged.Length > 0 ? src.PropertyInspectionFormElevation.DownSpoutsDamaged : string.Empty))
                .ForMember(dest => dest.DownspoutsDamageAmt, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormElevation.DownSpoutsDamaged.Length > 0 && src.PropertyInspectionFormElevation.DownSpoutsDamaged == "Yes" ?
                    (src.PropertyInspectionFormElevation.DownSpoutsNorthDamaged == "Yes" ? $" N ({src.PropertyInspectionFormElevation.DownSpoutsNorthLf.ToString()} LF)" : string.Empty) +
                    (src.PropertyInspectionFormElevation.DownSpoutsSouthDamaged == "Yes" ? $" S ({src.PropertyInspectionFormElevation.DownSpoutsSouthLf.ToString()} LF)" : string.Empty) +
                    (src.PropertyInspectionFormElevation.DownSpoutsEastDamaged == "Yes" ? $" E ({src.PropertyInspectionFormElevation.DownSpoutsEastLf.ToString()} LF)" : string.Empty) +
                    (src.PropertyInspectionFormElevation.DownSpoutsWestDamaged == "Yes" ? $" W ({src.PropertyInspectionFormElevation.DownSpoutsWestLf.ToString()} LF)" : string.Empty) : string.Empty))
                .ForMember(dest => dest.DownspoutsMaterial, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DownSpoutsMaterial, src.PropertyInspectionFormElevation.DownSpoutsMaterialOther)))
                .ForMember(dest => dest.DownSpoutsSize, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.DownSpoutsSize, src.PropertyInspectionFormElevation.DownSpoutsSizeOther)))
                .ForMember(dest => dest.GutterPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.GutterPresent))
                .ForMember(dest => dest.GutterDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.GuttersDamage.Length > 0 ? src.PropertyInspectionFormRoof.GuttersDamage : string.Empty))
                .ForMember(dest => dest.GutterDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.GuttersDamage.Length > 0 && src.PropertyInspectionFormRoof.GuttersDamage == "Yes" ? src.PropertyInspectionFormRoof.GutterDescription : string.Empty))
                .ForMember(dest => dest.GutterDamageAmt, opt => opt.MapFrom(src =>
                    (src.PropertyInspectionFormRoof.GutterNorthDamaged.HasValue && src.PropertyInspectionFormRoof.GutterNorthDamaged.Value ? $"North ({src.PropertyInspectionFormRoof.GutterNorthLf.ToString()} LF)" : string.Empty)
                    + (src.PropertyInspectionFormRoof.GutterSouthDamaged.HasValue && src.PropertyInspectionFormRoof.GutterSouthDamaged.Value ? $" South ({src.PropertyInspectionFormRoof.GutterSouthLf.ToString()} LF)" : string.Empty)
                    + (src.PropertyInspectionFormRoof.GutterEastDamaged.HasValue && src.PropertyInspectionFormRoof.GutterEastDamaged.Value ? $" East ({src.PropertyInspectionFormRoof.GutterEastLf.ToString()} LF)" : string.Empty)
                    + (src.PropertyInspectionFormRoof.GutterWestDamaged.HasValue && src.PropertyInspectionFormRoof.GutterWestDamaged.Value ? $" West ({src.PropertyInspectionFormRoof.GutterWestLf.ToString()} LF)" : string.Empty)))
                .ForMember(dest => dest.GutterMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.GutterMaterial == "Other" ? src.PropertyInspectionFormRoof.GutterMaterialOther : src.PropertyInspectionFormRoof.GutterMaterial))
                .ForMember(dest => dest.GutterSize, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.GutterSize, src.PropertyInspectionFormRoof.GutterSizeOther)))
                .ForMember(dest => dest.HandrailsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.HandrailDamaged))
                .ForMember(dest => dest.HandrailsMaterial, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.HandrailMaterial, src.PropertyInspectionFormElevation.HandrailMaterial)))
                .ForMember(dest => dest.HandrailsDamageAmt, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.HandrailDamaged == "Yes" ? src.PropertyInspectionFormElevation.HandrailLf.ToString() : string.Empty))
                .ForMember(dest => dest.HandrailPainted, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.HandrailPainted)))
                .ForMember(dest => dest.HandrailStained, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.HandrailStained)))
                .ForMember(dest => dest.WindowScreensDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowScreenDamaged))
                .ForMember(dest => dest.WindowScreensDamageAmt, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormElevation.WindowScreenDamaged == "Yes" ?
                    $"Small (1-9 SF) - {src.PropertyInspectionFormElevation.WindowScreenSmallSf.ToString()} " +
                    $"   Medium (10-16 SF) - {src.PropertyInspectionFormElevation.WindowScreenMediumSf.ToString()} " +
                    $"   Large (17-25 SF) -  {src.PropertyInspectionFormElevation.WindowScreenLargeSf.ToString()} " +
                    $"   X-Large (26-32 SF) - {src.PropertyInspectionFormElevation.WindowScreenXlargeSf.ToString()} " : string.Empty))
                .ForMember(dest => dest.SidingDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.SidingDamage))
                .ForMember(dest => dest.ContractorPresent, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormMain.ContractorPresent)))
                .ForMember(dest => dest.PublicAdjusterPresent, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormMain.PublicAdjusterPresent)))
                .ForMember(dest => dest.InsuredPresent, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormMain.InsuredPresent)))
                .ForMember(dest => dest.SoffitDepth, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.SoffitDepthLf)))
                .ForMember(dest => dest.SidingLength, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormElevation.SidingDamage == "Yes" ?
                    $"North ({src.PropertyInspectionFormElevation.SidingNorthQty.ToString()} SF) " +
                    $"South ({src.PropertyInspectionFormElevation.SidingSouthQty.ToString()} SF) " +
                    $"East ({src.PropertyInspectionFormElevation.SidingEastQty.ToString()} SF) " +
                    $"West ({src.PropertyInspectionFormElevation.SidingWestQty.ToString()} SF) " : string.Empty))
                .ForMember(dest => dest.SidingMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.SidingMaterial == "Other" ? src.PropertyInspectionFormElevation.SidingMaterialOther : src.PropertyInspectionFormElevation.SidingMaterial))
                .ForMember(dest => dest.SidingNotes, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormElevation.SidingNotes)))
                .ForMember(dest => dest.OutBuildingNotes, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.OutBuildingNotes)))
                .ForMember(dest => dest.OutBuildingDamage, opt => opt.MapFrom(src => MappingProfile.ReturnString(src.PropertyInspectionFormRoof.OutBuildingDamage)))
                // Damage Overview Section
                .ForMember(dest => dest.BrittlenessTestPerformed, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.BrittlenessTestConducted.HasValue && src.PropertyInspectionFormRoof.BrittlenessTestConducted.Value ? "Yes" : "No"))
                // Interior Section
                .ForMember(dest => dest.AdditionalFiveDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalFiveDamaged))
                .ForMember(dest => dest.AdditionalFiveDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalFiveDescription))
                .ForMember(dest => dest.AdditionalFiveSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalFiveSf.HasValue ? src.PropertyInspectionFormInterior.AdditionalFiveSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.AdditionalFourDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalFourDamaged))
                .ForMember(dest => dest.AdditionalFourDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalFourDescription))
                .ForMember(dest => dest.AdditionalFourSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalFourSf.HasValue ? src.PropertyInspectionFormInterior.AdditionalFourSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.AdditionalThreeDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalThreeDamaged))
                .ForMember(dest => dest.AdditionalThreeDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalThreeDescription))
                .ForMember(dest => dest.AdditionalThreeSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalThreeSf.HasValue ? src.PropertyInspectionFormInterior.AdditionalThreeSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.AdditionalTwoDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalTwoDamaged))
                .ForMember(dest => dest.AdditionalTwoDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalTwoDescription))
                .ForMember(dest => dest.AdditionalTwoSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalTwoSf.HasValue ? src.PropertyInspectionFormInterior.AdditionalTwoSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.AdditionalOneDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalOneDamaged))
                .ForMember(dest => dest.AdditionalOneDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalOneDescription))
                .ForMember(dest => dest.AdditionalOneSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AdditionalOneSf.HasValue ? src.PropertyInspectionFormInterior.AdditionalOneSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.AtticDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AtticDamaged))
                .ForMember(dest => dest.AtticDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AtticDescription))
                .ForMember(dest => dest.AtticSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.AtticSf.HasValue ? src.PropertyInspectionFormInterior.AtticSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BasementDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BasementDamaged))
                .ForMember(dest => dest.BasementDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BasementDescription))
                .ForMember(dest => dest.BasementSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BasementSf.HasValue ? src.PropertyInspectionFormInterior.BasementSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BathroomThreeDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BathroomThreeDamaged))
                .ForMember(dest => dest.BathroomThreeDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BathroomThreeDescription))
                .ForMember(dest => dest.BathroomThreeSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BathroomThreeSf.HasValue ? src.PropertyInspectionFormInterior.BathroomThreeSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BathroomTwoDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BathroomTwoDamaged))
                .ForMember(dest => dest.BathroomTwoDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BathroomTwoDescription))
                .ForMember(dest => dest.BathroomTwoSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BathroomTwoSf.HasValue ? src.PropertyInspectionFormInterior.BathroomTwoSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BedroomFourClosetDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomFourClosetDamaged))
                .ForMember(dest => dest.BedroomFourClosetDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomFourClosetDescription))
                .ForMember(dest => dest.BedroomFourClosetSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomFourClosetSf.HasValue ? src.PropertyInspectionFormInterior.BedroomFourClosetSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BedroomFourDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomFourDamaged))
                .ForMember(dest => dest.BedroomFourDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomFourDescription))
                .ForMember(dest => dest.BedroomFourSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomFourSf.HasValue ? src.PropertyInspectionFormInterior.BedroomFourSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BedroomThreeDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomThreeDamaged))
                .ForMember(dest => dest.BedroomThreeDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomThreeDescription))
                .ForMember(dest => dest.BedroomThreeSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomThreeSf.HasValue ? src.PropertyInspectionFormInterior.BedroomThreeSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BedroomTwoDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomTwoDamaged))
                .ForMember(dest => dest.BedroomTwoDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomTwoDescription))
                .ForMember(dest => dest.BedroomTwoSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BedroomTwoSf.HasValue ? src.PropertyInspectionFormInterior.BedroomTwoSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.BreakfastAreaDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BreakfastAreaDamaged))
                .ForMember(dest => dest.BreakfastAreaDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BreakfastAreaDescription))
                .ForMember(dest => dest.BreakfastAreaSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.BreakfastAreaSf.HasValue ? src.PropertyInspectionFormInterior.BreakfastAreaSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ContentsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.ContentsDamaged))
                .ForMember(dest => dest.ContentsDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.ContentsDescription))
                .ForMember(dest => dest.CrawlSpaceDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.CrawlSpaceDamaged))
                .ForMember(dest => dest.CrawlSpaceDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.CrawlSpaceDescription))
                .ForMember(dest => dest.CrawlSpaceSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.CrawlSpaceSf.HasValue ? src.PropertyInspectionFormInterior.CrawlSpaceSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.DiningRoomDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.DiningRoomDamaged))
                .ForMember(dest => dest.DiningRoomDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.DiningRoomDescription))
                .ForMember(dest => dest.DiningRoomSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.DiningRoomSf.HasValue ? src.PropertyInspectionFormInterior.DiningRoomSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.FamilyRoomDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.FamilyRoomDamaged))
                .ForMember(dest => dest.FamilyRoomDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.FamilyRoomDescription))
                .ForMember(dest => dest.FamilyRoomSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.FamilyRoomSf.HasValue ? src.PropertyInspectionFormInterior.FamilyRoomSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.FoyerDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.FoyerDamaged))
                .ForMember(dest => dest.FoyerDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.FoyerDescription))
                .ForMember(dest => dest.FoyerSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.FoyerSf.HasValue ? src.PropertyInspectionFormInterior.FoyerSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.GameRoomDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.GameRoomDamaged))
                .ForMember(dest => dest.GameRoomDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.GameRoomDescription))
                .ForMember(dest => dest.GameRoomSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.GameRoomSf.HasValue ? src.PropertyInspectionFormInterior.GameRoomSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.GarageDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.GarageDamaged))
                .ForMember(dest => dest.GarageDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.GarageDescription))
                .ForMember(dest => dest.GarageSf, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.GarageSf.HasValue ? src.PropertyInspectionFormElevation.GarageSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.HallClosetDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.HallClosetDamaged))
                .ForMember(dest => dest.HallClosetDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.HallClosetDescription))
                .ForMember(dest => dest.HallClosetSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.HallClosetSf.HasValue ? src.PropertyInspectionFormInterior.HallClosetSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.HallDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.HallDamaged))
                .ForMember(dest => dest.HallDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.HallDescription))
                .ForMember(dest => dest.HallSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.HallSf.HasValue ? src.PropertyInspectionFormInterior.HallSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.KitchenDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.KitchenDamaged))
                .ForMember(dest => dest.KitchenDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.KitchenDescription))
                .ForMember(dest => dest.KitchenSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.KitchenSf.HasValue ? src.PropertyInspectionFormInterior.KitchenSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.LivingRoomDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.LivingRoomDamaged))
                .ForMember(dest => dest.LivingRoomDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.LivingRoomDescription))
                .ForMember(dest => dest.LivingRoomSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.LivingRoomSf.HasValue ? src.PropertyInspectionFormInterior.LivingRoomSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.MasterBathroomDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterBathroomDamaged))
                .ForMember(dest => dest.MasterBathroomDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterBathroomDescription))
                .ForMember(dest => dest.MasterBathroomSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterBathroomSf.HasValue ? src.PropertyInspectionFormInterior.MasterBathroomSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.MasterBedroomDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterBedroomDamaged))
                .ForMember(dest => dest.MasterBedroomDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterBedroomDescription))
                .ForMember(dest => dest.MasterBedroomSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterBedroomSf.HasValue ? src.PropertyInspectionFormInterior.MasterBedroomSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.MasterClosetDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterClosetDamaged))
                .ForMember(dest => dest.MasterClosetDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterClosetDescription))
                .ForMember(dest => dest.MasterClosetSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.MasterClosetSf.HasValue ? src.PropertyInspectionFormInterior.MasterClosetSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.OfficeDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.OfficeDamaged))
                .ForMember(dest => dest.OfficeDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.OfficeDescription))
                .ForMember(dest => dest.OfficeSf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.OfficeSf.HasValue ? src.PropertyInspectionFormInterior.OfficeSf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.PantryDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.PantryDamaged))
                .ForMember(dest => dest.PantryDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.PantryDescription))
                .ForMember(dest => dest.PantrySf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.PantrySf.HasValue ? src.PropertyInspectionFormInterior.PantrySf.Value.ToString() : string.Empty))
                .ForMember(dest => dest.UtilityDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.UtilityDamaged))
                .ForMember(dest => dest.UtilityDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.UtilityDescription))
                .ForMember(dest => dest.UtilitySf, opt => opt.MapFrom(src => src.PropertyInspectionFormInterior.UtilitySf.HasValue ? src.PropertyInspectionFormInterior.UtilitySf.Value.ToString() : string.Empty))
                // Header Section
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Project.StreetAddress1))
                .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Project.StreetAddress2))
                .ForMember(dest => dest.AddressLine3, opt => opt.MapFrom(src => $"{src.Project.City}, {src.Project.State.Name} {src.Project.PostalCode}"))
                //  .ForMember(dest => dest.AddressLine3, opt => opt.MapFrom(src => $"{src.Project.City},{src.Project.State.Name}, {src.Project.PostalCode}"))
                .ForMember(dest => dest.Adjuster, opt => opt.MapFrom(src => src.Project.Adjuster.FirstName + " " + src.Project.Adjuster.LastName))
                .ForMember(dest => dest.Carrier, opt => opt.MapFrom(src => src.Project.Company.FullName))
                .ForMember(dest => dest.ClaimNumber, opt => opt.MapFrom(src => src.Project.Claim.ClaimNumber))
                .ForMember(dest => dest.InsuredName, opt => opt.MapFrom(src => src.Project.InsuredFirstName + " " + src.Project.InsuredLastName))
                .ForMember(dest => dest.ProjectNumber, opt => opt.MapFrom(src => src.Project.ProjectNumber))
                .ForMember(dest => dest.InspectionDate, opt => opt.MapFrom(src => src.Project.ProjectInspections.Count() > 0 ? src.Project.ProjectInspections.ElementAt(0).ScheduleDate.ToString("MM/dd/yyyy h:mm tt") : "NA"))
                // Non Storm Damage Items
                .ForMember(dest => dest.BlisterDamage, opt => opt.MapFrom(src =>
                    (src.PropertyInspectionFormRoof.BlistersNorth == "Yes" ? "North Blisters|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.BlistersSouth == "Yes" ? "South Blisters|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.BlistersEast == "Yes" ? "East Blisters|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.BlistersWest == "Yes" ? "West Blisters|" : string.Empty)))
                .ForMember(dest => dest.CloggedGutterDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.CloggedGuttersNorth == "Yes" ? "North Clogged Gutters|" : string.Empty
                    + (src.PropertyInspectionFormRoof.CloggedGuttersSouth == "Yes" ? "South Clogged Gutters|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.CloggedGuttersEast == "Yes" ? "East Clogged Gutters|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.CloggedGuttersWest == "Yes" ? "West Clogged Gutters|" : string.Empty)))
                .ForMember(dest => dest.CuppedDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.CuppedNorth == "Yes" ? "North Cupped/Curled|" : string.Empty
                    + (src.PropertyInspectionFormRoof.CuppedSouth == "Yes" ? "South Cupped/Curled|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.CuppedEast == "Yes" ? "East Cupped/Curled|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.CuppedWest == "Yes" ? "West Cupped/Curled|" : string.Empty)))
                .ForMember(dest => dest.DeckingRottedDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.DeckingRottedNorth == "Yes" ? "North Decking Rotted|" : string.Empty
                    + (src.PropertyInspectionFormRoof.DeckingRottedSouth == "Yes" ? "South Decking Rotted|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.DeckingRottedEast == "Yes" ? "East Decking Rotted|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.DeckingRottedWest == "Yes" ? "West Decking Rotted|" : string.Empty)))
                .ForMember(dest => dest.ExcessiveWeatheringDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.ExcessiveWeathering.HasValue && src.PropertyInspectionFormRoof.ExcessiveWeathering.Value && src.PropertyInspectionFormRoof.ExcessiveWeathering.Value ? "Excessive Weathering|" : string.Empty))
                .ForMember(dest => dest.ExposedFastenersDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.ExposedFasteners.HasValue && src.PropertyInspectionFormRoof.ExposedFasteners.Value && src.PropertyInspectionFormRoof.ExposedFasteners.Value ? "Exposed Fasteners|" : string.Empty))
                .ForMember(dest => dest.FlashingNotSealedDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.FlashingNotSealedNorth == "Yes" ? "North Flashing Not Sealed|" : string.Empty
                    + (src.PropertyInspectionFormRoof.FlashingNotSealedSouth == "Yes" ? "South Flashing Not Sealed|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.FlashingNotSealedEast == "Yes" ? "East Flashing Not Sealed|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.FlashingNotSealedWest == "Yes" ? "West Flashing Not Sealed|" : string.Empty)))
                .ForMember(dest => dest.GranuleLossDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.GranuleLossNorth == "Yes" ? "North Granule Loss|" : string.Empty
                    + (src.PropertyInspectionFormRoof.GranuleLossSouth == "Yes" ? "South Granule Loss|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.GranuleLossEast == "Yes" ? "East Granule Loss|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.GranuleLossWest == "Yes" ? "West Granule Loss|" : string.Empty)))
                .ForMember(dest => dest.InstallationErrorDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.InstallationError.HasValue && src.PropertyInspectionFormRoof.InstallationError.Value ? "Installation Error|" : string.Empty))
                .ForMember(dest => dest.InstallationErrorDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.InstallationError.HasValue && src.PropertyInspectionFormRoof.InstallationError.Value ? src.PropertyInspectionFormRoof.InstallationErrorDescription == "Other" ? src.PropertyInspectionFormRoof.InstallationErrorDescriptionOther : src.PropertyInspectionFormRoof.InstallationErrorDescription : string.Empty))
                .ForMember(dest => dest.ManufacturedMarksDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.ManufacturedMarksNorth == "Yes" ? "North Manufactured Marks|" : string.Empty
                    + (src.PropertyInspectionFormRoof.ManufacturedMarksSouth == "Yes" ? "South Manufactured Marks|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.ManufacturedMarksEast == "Yes" ? "East Manufactured Marks|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.ManufacturedMarksWest == "Yes" ? "West Manufactured Marks|" : string.Empty)))
                .ForMember(dest => dest.MechanicalMarksDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.MechanicalMarksNorth == "Yes" ? "North Mechanical Marks|" : string.Empty
                    + (src.PropertyInspectionFormRoof.MechanicalMarksSouth == "Yes" ? "South Mechanical Marks|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.MechanicalMarksEast == "Yes" ? "East Mechanical Marks|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.MechanicalMarksWest == "Yes" ? "West Mechanical Marks|" : string.Empty)))
                .ForMember(dest => dest.NailPopsDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.NailPopsNorth == "Yes" ? "North Nail Pops|" : string.Empty
                    + (src.PropertyInspectionFormRoof.NailPopsSouth == "Yes" ? "South Nail Pops|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.NailPopsEast == "Yes" ? "East Nail Pops|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.NailPopsWest == "Yes" ? "West Nail Pops|" : string.Empty)))
                .ForMember(dest => dest.RodentDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RodentDamage.HasValue && src.PropertyInspectionFormRoof.RodentDamage.Value ? "Rodent Damage|" : string.Empty))
                .ForMember(dest => dest.RoofDebrisDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.RoofDebrisNorth == "Yes" ? "North Roof Debris|" : string.Empty
                    + (src.PropertyInspectionFormRoof.RoofDebrisSouth == "Yes" ? "South Roof Debris|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.RoofDebrisEast == "Yes" ? "East Roof Debris|" : string.Empty)
                    + (src.PropertyInspectionFormRoof.RoofDebrisWest == "Yes" ? "West Roof Debris|" : string.Empty)))
                .ForMember(dest => dest.SpatterMarksDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.SpatterMarks.HasValue && src.PropertyInspectionFormRoof.SpatterMarks.Value ? "Spatter Marks|" : string.Empty))
                .ForMember(dest => dest.ThermalCrackingDamage, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormRoof.ThermalCrackingNorth == "Yes" ? "North Thermal Cracking|" : string.Empty
                        + (src.PropertyInspectionFormRoof.ThermalCrackingSouth == "Yes" ? "South Thermal Cracking|" : string.Empty)
                        + (src.PropertyInspectionFormRoof.ThermalCrackingEast == "Yes" ? "East Thermal Cracking|" : string.Empty)
                        + (src.PropertyInspectionFormRoof.ThermalCrackingWest == "Yes" ? "West Thermal Cracking|" : string.Empty)))
                .ForMember(dest => dest.PipebootLeadPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.LeadPipebootPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.LeadPipebootPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.PipebootLeadQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.LeadPipebootQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.LeadPipebootQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.PipebootLeadQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.LeadPipebootQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.LeadPipebootQty.ToString() : string.Empty))
                .ForMember(dest => dest.PipebootLeadPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.LeadPipebootPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.LeadPipebootPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.PipebootPlasticPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.PipebootPlasticQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.PipebootPlasticQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootQty.ToString() : string.Empty))
                .ForMember(dest => dest.PipebootPlasticPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PlasticPipebootPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.BoxVentPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.BoxVentsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.BoxVentsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.BoxVentDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.BoxVentsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.BoxVentsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.BoxVentQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.BoxVentsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.BoxVentsQty.ToString() : string.Empty))
                .ForMember(dest => dest.BoxVentMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.BoxVentsMaterial.Length > 0 ? src.PropertyInspectionFormRoofDamagedItem.BoxVentsMaterial : string.Empty))
                .ForMember(dest => dest.BoxVentPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.BoxVentsPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.BoxVentsPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.CanVentPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.CanVentsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.CanVentsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.CanVentsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.CanVentsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.CanVentsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.CanVentsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.CanVentsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.CanVentsQty.ToString() : string.Empty))
                .ForMember(dest => dest.CanVentsPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.CanVentsPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.CanVentsPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.ChimneyCapLargePresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargePresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargePresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.ChimneyCapLargeDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargeQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargeQtyDamaged.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ChimneyCapLargeQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargeQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargeQty.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ChimneyCapLargePainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargePainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapLargePainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.ChimneyCapMediumPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.ChimneyCapMediumDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumQtyDamaged.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ChimneyCapMediumQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumQty.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ChimneyCapMediumPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapMediumPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.ChimneyCapSmallPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.ChimneyCapSmallDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallQtyDamaged.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ChimneyCapSmallQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallQty.Value.ToString() : string.Empty))
                .ForMember(dest => dest.ChimneyCapSmallPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ChimneyCapSmallPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.DripEdgeRakePresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakePresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakePresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.DripEdgeRakeDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakeQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakeQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.DripEdgeRakeQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakeQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakeQty.ToString() + " LF" : string.Empty))
                .ForMember(dest => dest.DripEdgeRakePainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakePainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DripEdgeRakePainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.DripEdgeEavePresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeEavePresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.DripEdgeEavePresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.DripEdgeEaveDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeEaveQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DripEdgeEaveQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.DripEdgeEaveQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeEaveQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DripEdgeEaveQty.ToString() + " LF" : string.Empty))
                .ForMember(dest => dest.DripEdgeEavePainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DripEdgeEavePainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DripEdgeEavePainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.DryerExhaustPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DryerExhaustPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.DryerExhaustPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.DryerExhaustQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DryerExhaustQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DryerExhaustQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.DryerExhaustQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DryerExhaustQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DryerExhaustQty.ToString() : string.Empty))
                .ForMember(dest => dest.DryerExhaustPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.DryerExhaustPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.DryerExhaustPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.FlashingPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlashingPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.FlashingPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.FlashingDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlashingQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.FlashingQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.FlashingQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlashingQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.FlashingQty.ToString() : string.Empty))
                .ForMember(dest => dest.FlashingPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlashingPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.FlashingPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.FlatRoofScupperDrainPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.FlatRoofScupperDrainQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.FlatRoofScupperDrainQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainQty.ToString() : string.Empty))
                .ForMember(dest => dest.FlatRoofScupperDrainPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.FlatRoofScupperDrainPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.GableVentPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.GableVentPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.GableVentPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.GableVentDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.GableVentQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.GableVentQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.GableVentQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.GableVentQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.GableVentQty.ToString() : string.Empty))
                .ForMember(dest => dest.HvacEightPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacEightPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.HvacEightPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.HvacEightDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacEightQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacEightQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.HvacEightQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacEightQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacEightQty.ToString() : string.Empty))
                .ForMember(dest => dest.HvacEightPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacEightPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacEightPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.HvacFourPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacFourPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.HvacFourPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.HvacFourDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacFourQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacFourQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.HvacFourQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacFourQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacFourQty.ToString() : string.Empty))
                .ForMember(dest => dest.HvacFourPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacFourPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacFourPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.HvacSixPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacSixPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.HvacSixPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.HvacSixDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacSixQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacSixQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.HvacSixQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacSixQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacSixQty.ToString() : string.Empty))
                .ForMember(dest => dest.HvacSixPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacSixPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacSixPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.HvacOtherPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacOtherPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.HvacOtherPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.HvacOtherQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacOtherQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacOtherQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.HvacOtherQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacOtherQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacOtherQty.ToString() : string.Empty))
                .ForMember(dest => dest.HvacOtherPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacOtherPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacOtherPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.HvacOtherSize, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.HvacOtherSize.HasValue ? src.PropertyInspectionFormRoofDamagedItem.HvacOtherSize.ToString() : string.Empty))
                .ForMember(dest => dest.MastheadPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.MastheadPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.MastheadPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.MastheadDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.MastheadQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.MastheadQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.MastheadQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.MastheadQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.MastheadQty.ToString() : string.Empty))
                .ForMember(dest => dest.MastheadPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.MastheadPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.MastheadPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.OffRidgeVentsPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.OffRidgeVentsQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.OffRidgeVentsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsQty.ToString() : string.Empty))
                .ForMember(dest => dest.OffRidgeVentsMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsMaterial.Length > 0 ? src.PropertyInspectionFormRoofDamagedItem.OffRidgeVentsMaterial : string.Empty))
                .ForMember(dest => dest.OtherPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OtherPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.OtherPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.OtherDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OtherQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.OtherQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.OtherQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.OtherQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.OtherQty.ToString() : string.Empty))
                .ForMember(dest => dest.PowerVentsPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.PowerVentsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.PowerVentsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PowerVentsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.PowerVentsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PowerVentsQty.ToString() : string.Empty))
                .ForMember(dest => dest.PowerVentsMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentsMaterial.Length > 0 ? src.PropertyInspectionFormRoofDamagedItem.PowerVentsMaterial : string.Empty))
                .ForMember(dest => dest.PowerVentsPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentsPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PowerVentsPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.PowerVentCoversPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.PowerVentCoversDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.PowerVentCoversQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversQty.ToString() : string.Empty))
                .ForMember(dest => dest.PowerVentCoversMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversMaterial.Length > 0 ? src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversMaterial : string.Empty))
                .ForMember(dest => dest.PowerVentCoversPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.PowerVentCoversPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.RainDiverterPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RainDivertersPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.RainDivertersPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.RainDiverterDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RainDivertersQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.RainDivertersQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.RainDiverterQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RainDivertersQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.RainDivertersQty.ToString() : string.Empty))
                .ForMember(dest => dest.RidgeVentsPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RidgeVentsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.RidgeVentsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.RidgeVentsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RidgeVentsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.RidgeVentsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.RidgeVentsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RidgeVentsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.RidgeVentsQty.ToString() : string.Empty))
                .ForMember(dest => dest.RidgeVentsMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RidgeVentsMaterial.Length > 0 ? src.PropertyInspectionFormRoofDamagedItem.RidgeVentsMaterial : string.Empty))
                .ForMember(dest => dest.RidgeVentsPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.RidgeVentsPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.RidgeVentsPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.SatelliteDishPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SatelliteDishPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.SatelliteDishPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.SatelliteDishDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SatelliteDishQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SatelliteDishQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.SatelliteDishQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SatelliteDishQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SatelliteDishQty.ToString() : string.Empty))
                .ForMember(dest => dest.SkylightsPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SkylightsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.SkylightsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.SkylightsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SkylightsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SkylightsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.SkylightsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SkylightsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SkylightsQty.ToString() : string.Empty))
                .ForMember(dest => dest.SkylightsPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SkylightsPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SkylightsPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.SoffitVentsPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SoffitVentsPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.SoffitVentsPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.SoffitVentsDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SoffitVentsQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SoffitVentsQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.SoffitVentsQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SoffitVentsQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SoffitVentsQty.ToString() : string.Empty))
                .ForMember(dest => dest.SoffitVentsPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SoffitVentsPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SoffitVentsPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.SolarPanelPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SolarPanelPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.SolarPanelPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.SolarPanelQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SolarPanelQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SolarPanelQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.SolarPanelQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SolarPanelQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SolarPanelQty.ToString() : string.Empty))
                .ForMember(dest => dest.SolarPanelPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SolarPanelPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SolarPanelPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.SwampCoolerPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SwampCoolerPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.SwampCoolerPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.SwampCoolerQtyDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SwampCoolerQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SwampCoolerQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.SwampCoolerQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SwampCoolerQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SwampCoolerQty.ToString() : string.Empty))
                .ForMember(dest => dest.SwampCoolerPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.SwampCoolerPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.SwampCoolerPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.TurbinePresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.TurbinesPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.TurbinesPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.TurbineDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.TurbinesQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.TurbinesQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.TurbineQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.TurbinesQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.TurbinesQty.ToString() : string.Empty))
                .ForMember(dest => dest.TurbinePainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.TurbinesPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.TurbinesPainted.Value ? "Yes" : "No" : string.Empty))
                .ForMember(dest => dest.ValleyMetalPresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ValleyMetalPresent.HasValue && src.PropertyInspectionFormRoofDamagedItem.ValleyMetalPresent.Value ? "Yes" : "No"))
                .ForMember(dest => dest.ValleyMetalDamaged, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ValleyMetalQtyDamaged.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ValleyMetalQtyDamaged.ToString() : string.Empty))
                .ForMember(dest => dest.ValleyMetalQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ValleyMetalQty.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ValleyMetalQty.ToString() + " LF" : string.Empty))
                .ForMember(dest => dest.ValleyMetalMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ValleyMetalMaterial.Length > 0 ? src.PropertyInspectionFormRoofDamagedItem.ValleyMetalMaterial : string.Empty))
                .ForMember(dest => dest.ValleyMetalPainted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoofDamagedItem.ValleyMetalPainted.HasValue ? src.PropertyInspectionFormRoofDamagedItem.ValleyMetalPainted.Value ? "Yes" : "No" : string.Empty))
                // Roof Summary
                .ForMember(dest => dest.FeltDescription, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.FeltDescription.Length > 0 ? src.PropertyInspectionFormRoof.FeltDescription : string.Empty))
                .ForMember(dest => dest.IceWaterShield, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.IceWaterShield.HasValue && src.PropertyInspectionFormRoof.IceWaterShield.Value ? "Yes" : "No"))
                .ForMember(dest => dest.LayerQty, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.LayerQty.HasValue ? src.PropertyInspectionFormRoof.LayerQty.ToString() : string.Empty))
                .ForMember(dest => dest.Rake, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RakeStarter.HasValue && src.PropertyInspectionFormRoof.RakeStarter.Value ? "Yes" : "No"))
                .ForMember(dest => dest.RoofAgeYears, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofAgeYears.Length > 0 ? src.PropertyInspectionFormRoof.RoofAgeYears.ToString() : string.Empty))
                .ForMember(dest => dest.RoofMaterial, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofMaterial == "Other" ? src.PropertyInspectionFormRoof.RoofMaterialOther : src.PropertyInspectionFormRoof.RoofMaterial))
                .ForMember(dest => dest.RoofPitchSevenSqft, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofPitchSevenSqft.HasValue ? src.PropertyInspectionFormRoof.RoofPitchSevenSqft.ToString() : string.Empty))
                .ForMember(dest => dest.RoofPitchTenSqft, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofPitchTenSqft.HasValue ? src.PropertyInspectionFormRoof.RoofPitchTenSqft.ToString() : string.Empty))
                .ForMember(dest => dest.RoofPitchZeroSqft, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofPitchZeroSqft.HasValue ? src.PropertyInspectionFormRoof.RoofPitchZeroSqft.ToString() : string.Empty))
                .ForMember(dest => dest.RoofStoryOneSqft, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofStoryOneSqft.HasValue ? src.PropertyInspectionFormRoof.RoofPitchZeroSqft.ToString() : string.Empty))
                .ForMember(dest => dest.RoofStoryTwoSqft, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofStoryTwoSqft.HasValue ? src.PropertyInspectionFormRoof.RoofStoryTwoSqft.ToString() : string.Empty))
                .ForMember(dest => dest.RoofType, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RoofType.Length > 0 ? src.PropertyInspectionFormRoof.RoofType.ToString() : string.Empty))
                .ForMember(dest => dest.Stories, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.StoriesOther.HasValue ? src.PropertyInspectionFormRoof.StoriesOther.ToString() : src.PropertyInspectionFormRoof.Stories.ToString()))
                .ForMember(dest => dest.UniversalStarter, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.RakeStarter.HasValue && src.PropertyInspectionFormRoof.RakeStarter.Value && src.PropertyInspectionFormRoof.EaveStarter.HasValue && src.PropertyInspectionFormRoof.EaveStarter.Value ? "Yes" : "No"))
                .ForMember(dest => dest.ValleyType, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.ValleyType.Length > 0 ? src.PropertyInspectionFormRoof.ValleyType : string.Empty))
                // Roof Observation
                .ForMember(dest => dest.BrittlenessTestConducted, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.BrittlenessTestConducted.HasValue && src.PropertyInspectionFormRoof.BrittlenessTestConducted.Value ? "Yes" : "No"))
                .ForMember(dest => dest.ElevationDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormElevation.WindowsDamaged == "Yes"
                    || src.PropertyInspectionFormElevation.SidingDamage == "Yes"
                    || (src.PropertyInspectionFormElevation.FasciaDamaged.Length > 0 && src.PropertyInspectionFormElevation.FasciaDamaged == "Yes")
                    || (src.PropertyInspectionFormRoof.GuttersDamage.Length > 0 && src.PropertyInspectionFormRoof.GuttersDamage == "Yes")
                    || (src.PropertyInspectionFormElevation.DownSpoutsDamaged.Length > 0 && src.PropertyInspectionFormElevation.DownSpoutsDamaged == "Yes") ? "Yes" : "No"))
                .ForMember(dest => dest.HailDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.HailDamageEastSf.HasValue && src.PropertyInspectionFormRoof.HailDamageEastSf > 0
                    || src.PropertyInspectionFormRoof.HailDamageWestSf.HasValue && src.PropertyInspectionFormRoof.HailDamageWestSf > 0
                    || src.PropertyInspectionFormRoof.HailDamageNorthSf.HasValue && src.PropertyInspectionFormRoof.HailDamageNorthSf > 0
                    || src.PropertyInspectionFormRoof.HailDamageSouthSf.HasValue && src.PropertyInspectionFormRoof.HailDamageSouthSf > 0 ? "Yes" : "No"))
                .ForMember(dest => dest.PitchFront, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ? src.PropertyInspectionFormRoof.PitchNorth == "Other" ? src.PropertyInspectionFormRoof.PitchNorthOther : src.PropertyInspectionFormRoof.PitchNorth
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ? src.PropertyInspectionFormRoof.PitchSouth == "Other" ? src.PropertyInspectionFormRoof.PitchSouthOther : src.PropertyInspectionFormRoof.PitchSouth
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ? src.PropertyInspectionFormRoof.PitchWest == "Other" ? src.PropertyInspectionFormRoof.PitchWestOther : src.PropertyInspectionFormRoof.PitchWest
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ? src.PropertyInspectionFormRoof.PitchEast == "Other" ? src.PropertyInspectionFormRoof.PitchEastOther : src.PropertyInspectionFormRoof.PitchEast : string.Empty))
                .ForMember(dest => dest.PitchBack, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                        src.PropertyInspectionFormRoof.PitchSouth == "Other" ? src.PropertyInspectionFormRoof.PitchSouthOther : src.PropertyInspectionFormRoof.PitchSouth
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                        src.PropertyInspectionFormRoof.PitchNorth == "Other" ? src.PropertyInspectionFormRoof.PitchNorthOther : src.PropertyInspectionFormRoof.PitchNorth
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                        src.PropertyInspectionFormRoof.PitchEast == "Other" ? src.PropertyInspectionFormRoof.PitchEastOther : src.PropertyInspectionFormRoof.PitchEast
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                            src.PropertyInspectionFormRoof.PitchWest == "Other" ? src.PropertyInspectionFormRoof.PitchWestOther : src.PropertyInspectionFormRoof.PitchWest : string.Empty))
                .ForMember(dest => dest.PitchLeft, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                        src.PropertyInspectionFormRoof.PitchEast == "Other" ? src.PropertyInspectionFormRoof.PitchEastOther : src.PropertyInspectionFormRoof.PitchEast
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                            src.PropertyInspectionFormRoof.PitchWest == "Other" ? src.PropertyInspectionFormRoof.PitchWestOther : src.PropertyInspectionFormRoof.PitchWest
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                        src.PropertyInspectionFormRoof.PitchNorth == "Other" ? src.PropertyInspectionFormRoof.PitchNorthOther : src.PropertyInspectionFormRoof.PitchNorth
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                        src.PropertyInspectionFormRoof.PitchSouth == "Other" ? src.PropertyInspectionFormRoof.PitchSouthOther : src.PropertyInspectionFormRoof.PitchSouth : string.Empty))
                .ForMember(dest => dest.PitchRight, opt => opt.MapFrom(src =>
                    src.PropertyInspectionFormMain.DirectionNorth == "Front" ?
                            src.PropertyInspectionFormRoof.PitchWest == "Other" ? src.PropertyInspectionFormRoof.PitchWestOther : src.PropertyInspectionFormRoof.PitchWest
                    : src.PropertyInspectionFormMain.DirectionNorth == "Back" ?
                        src.PropertyInspectionFormRoof.PitchEast == "Other" ? src.PropertyInspectionFormRoof.PitchEastOther : src.PropertyInspectionFormRoof.PitchEast
                    : src.PropertyInspectionFormMain.DirectionNorth == "Left" ?
                        src.PropertyInspectionFormRoof.PitchSouth == "Other" ? src.PropertyInspectionFormRoof.PitchSouthOther : src.PropertyInspectionFormRoof.PitchSouth
                    : src.PropertyInspectionFormMain.DirectionNorth == "Right" ?
                        src.PropertyInspectionFormRoof.PitchNorth == "Other" ? src.PropertyInspectionFormRoof.PitchNorthOther : src.PropertyInspectionFormRoof.PitchNorth : string.Empty))
                .ForMember(dest => dest.WindDamage, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.StormDamageType.Contains("Wind") ? "Yes" : "No"))
                // Storm Direction
                .ForMember(dest => dest.StormDamagePresent, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.StormDamage.HasValue && src.PropertyInspectionFormRoof.StormDamage.Value ? "Yes" : "No"))
                .ForMember(dest => dest.StormDamageType, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.StormDamageType.Length > 0 ? src.PropertyInspectionFormRoof.StormDamageType : string.Empty))
                .ForMember(dest => dest.StormDirection, opt => opt.MapFrom(src => src.PropertyInspectionFormRoof.StormDirection.Length > 0 ? src.PropertyInspectionFormRoof.StormDirection : string.Empty))
                .ForAllOtherMembers(vm => vm.Ignore());
        }
    }
}
