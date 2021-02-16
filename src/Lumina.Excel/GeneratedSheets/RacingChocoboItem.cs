// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "RacingChocoboItem", columnHash: 0xe79dd9d4 )]
    public class RacingChocoboItem : ExcelRow
    {
        
        public LazyRow< Item > Item;
        public byte Category;
        public byte[] Param;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Item = new LazyRow< Item >( gameData, parser.ReadColumn< int >( 0 ), language );
            Category = parser.ReadColumn< byte >( 1 );
            Param = new byte[ 2 ];
            for( var i = 0; i < 2; i++ )
                Param[ i ] = parser.ReadColumn< byte >( 2 + i );
        }
    }
}