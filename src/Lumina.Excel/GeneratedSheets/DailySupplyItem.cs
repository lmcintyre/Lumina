// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "DailySupplyItem", columnHash: 0x5e7b2507 )]
    public class DailySupplyItem : ExcelRow
    {
        public struct UnkStruct0Struct
        {
            public int Item;
            public byte Quantity;
            public byte RecipeLevel;
        }
        
        public UnkStruct0Struct[] UnkStruct0 { get; set; }
        
        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            UnkStruct0 = new UnkStruct0Struct[ 8 ];
            for( var i = 0; i < 8; i++ )
            {
                UnkStruct0[ i ] = new UnkStruct0Struct();
                UnkStruct0[ i ].Item = parser.ReadColumn< int >( 0 + ( i * 3 + 0 ) );
                UnkStruct0[ i ].Quantity = parser.ReadColumn< byte >( 0 + ( i * 3 + 1 ) );
                UnkStruct0[ i ].RecipeLevel = parser.ReadColumn< byte >( 0 + ( i * 3 + 2 ) );
            }
        }
    }
}