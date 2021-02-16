// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "ActionComboRoute", columnHash: 0xc4b3400f )]
    public class ActionComboRoute : ExcelRow
    {
        
        public SeString Name;
        public sbyte Unknown1;
        public LazyRow< Action >[] Action;
        public bool Unknown6;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            Name = parser.ReadColumn< SeString >( 0 );
            Unknown1 = parser.ReadColumn< sbyte >( 1 );
            Action = new LazyRow< Action >[ 4 ];
            for( var i = 0; i < 4; i++ )
                Action[ i ] = new LazyRow< Action >( gameData, parser.ReadColumn< ushort >( 2 + i ), language );
            Unknown6 = parser.ReadColumn< bool >( 6 );
        }
    }
}