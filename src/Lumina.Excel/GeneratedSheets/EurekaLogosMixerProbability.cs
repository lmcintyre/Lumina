// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "EurekaLogosMixerProbability", columnHash: 0xdcfd9eba )]
    public class EurekaLogosMixerProbability : ExcelRow
    {
        
        public byte ProbabilityPct;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            ProbabilityPct = parser.ReadColumn< byte >( 0 );
        }
    }
}