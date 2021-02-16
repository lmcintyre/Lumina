// ReSharper disable All

using Lumina.Text;
using Lumina.Data;
using Lumina.Data.Structs.Excel;

namespace Lumina.Excel.GeneratedSheets
{
    [Sheet( "MateriaJoinRateGatherCraft", columnHash: 0xab31b42e )]
    public class MateriaJoinRateGatherCraft : ExcelRow
    {
        
        public float[] NQOvermeldPctSlot;
        public float[] HQOvermeldPctSlot;
        

        public override void PopulateData( RowParser parser, GameData gameData, Language language )
        {
            base.PopulateData( parser, gameData, language );

            NQOvermeldPctSlot = new float[ 4 ];
            for( var i = 0; i < 4; i++ )
                NQOvermeldPctSlot[ i ] = parser.ReadColumn< float >( 0 + i );
            HQOvermeldPctSlot = new float[ 4 ];
            for( var i = 0; i < 4; i++ )
                HQOvermeldPctSlot[ i ] = parser.ReadColumn< float >( 4 + i );
        }
    }
}