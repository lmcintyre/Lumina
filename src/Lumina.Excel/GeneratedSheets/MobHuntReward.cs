// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "MobHuntReward", columnHash: 0x4ace707c )]
    public class MobHuntReward : ExcelRow
    {
        
        public uint ExpReward;
        public ushort GilReward;
        public LazyRow< ExVersion > Expansion;
        public ushort CurrencyReward;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            ExpReward = parser.ReadColumn< uint >( 0 );
            GilReward = parser.ReadColumn< ushort >( 1 );
            Expansion = new LazyRow< ExVersion >( gameData, parser.ReadColumn< byte >( 2 ), language );
            CurrencyReward = parser.ReadColumn< ushort >( 3 );
        }
    }
}