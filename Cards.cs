using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace cards
{
  public enum eSUIT : int
  {
    CLUBS   = 0,
    DIAMOND = 1,
    HEARTS  = 2,
    SPADES  = 3
  }

  public enum eRank : int
  {
    ACE   = 0,
    TWO   = 1,
    THREE = 2,
    FOUR  = 3,
    FIVE  = 4,
    SIX   = 5,
    SEVEN = 6,
    EIGHT = 7,
    NINE  = 8,
    TEN   = 9,
    JACK  = 10,
    QUEEN = 11,
    KING  = 12
  }

  public enum eBACK : int
  {
    CROSSHATCH = 53, /* XP = CROSSHATCH */ 
    WEAVE1     = 54, /* XP = SKY */
    WEAVE2     = 55, /* XP = MINERAL */
    ROBOT      = 56, /* XP = FISH */
    FLOWERS    = 57, /* XP = FROG */
    VINE1      = 58, /* XP = MOONFLOWER */
    VINE2      = 59, /* XP = ISLAND */
    FISH1      = 60, /* XP = SQUARES */
    FISH2      = 61, /* XP = MAGENTA */
    SHELLS     = 62, /* XP = SANDDUNES */
    CASTLE     = 63, /* XP = SPACE */
    ISLAND     = 64, /* XP = LINES */
    CARDHAND   = 65, /* XP = TOYCARS */
    UNUSED     = 66, /* XP = UNUSED */
    THE_X      = 67, /* XP = THE_X */
    THE_O      = 68  /* XP = THE_0 */
  }

  public class cardsdll
  {
    /*****************************************************************************************
    * Interface to cards.dll                                                                 *
    *****************************************************************************************/

    [DllImport("cards.dll")]
    private static extern bool cdtInit( ref int width, ref int height );

    [DllImport("cards.dll")]
    private static extern void cdtTerm();

    [DllImport("cards.dll")]
    private static extern bool cdtDraw( IntPtr hdc, int x, int y, int card, int mode, long color );

    [DllImport("cards.dll")]
    private static extern bool cdtDrawExt( IntPtr hdc, int x, int y, int dx, int dy, int card, int mode, long color );

    [DllImport("cards.dll")]
    private static extern bool cdtAnimate( IntPtr hdc, int cardback, int x, int y, int frame );

    /*****************************************************************************************
    * Constant declarations                                                                  *
    *****************************************************************************************/

    /* mode parameters */
    const int mdFaceUp         = 0; /* Draw card face up */
    const int mdFaceDown       = 1; /* Draw card face down */
    const int mdHilite         = 2; /* Same as FaceUp except drawn inverted */
    const int mdGhost          = 3; /* Draw a ghost card -- for ace piles */
    const int mdRemove         = 4; /* draw background specified by color */
    const int mdInvisibleGhost = 5; /* ? */
    const int mdDeckX          = 6; /* Draw X */
    const int mdDeckO          = 7; /* Draw O */

    /*****************************************************************************************
    * Public Interface                                                                       *
    *****************************************************************************************/

    public cardsdll()
    {
      int width = 71;
      int height = 95;
      if( !cdtInit(ref width, ref height) )
    	throw new Exception ( "cards.dll did not load" );
    }

    public void Dispose()
    {
      cdtTerm();
    }

    public bool drawCard( IntPtr hdc, int x, int y, int card, int mode, long color )
    {
      return cdtDraw( hdc, x, y, card, mode, color );				
    }

    public bool drawCardBack( IntPtr hdc, int x, int y, eBACK back )
    {
      return cdtDraw( hdc, x, y, (int)back, mdFaceDown, 0 );				
    }

    public bool drawAnimatedBack( IntPtr hdc, int x, int y, int card, int frame )
    {
      return cdtAnimate( hdc, card, x, y, frame );
    }

    public bool drawInvertedCard( IntPtr hdc, int x, int y, int card )
    {
      return cdtDraw( hdc, x, y, card, mdHilite, 0 );				
    }

    public bool drawEmptyCard( IntPtr hdc, int x, int y, long color )
    {
      return cdtDraw( hdc, x, y, 1, mdGhost, color );			
    }

    public bool drawExtrudedCard( IntPtr hdc, int x, int y, int dx, int dy, int card, int mode, long color )
    {
      return cdtDrawExt( hdc, x, y, dx, dy, card, mode, color );
    }

    public static eSUIT getSuit( int card )
    {
      int suit = card % 4;
      return (eSUIT)suit;
    }

    public static eRank getRank( int card )
    {
      int rank = card / 4;
      return (eRank)rank;
    }
  }
}