using System;
using System.Drawing;
using System.Windows.Forms;

namespace cards
{
  public class CardDisplay : Form
  {
    private System.ComponentModel.Container components = null;
    private cards.cardsdll cardHandle;
    private IntPtr hdc = new IntPtr();

    // Constructor
    public CardDisplay()
    {
      InitializeComponent();

      cardHandle = new cards.cardsdll();
    }
  
    // Clean up any resources being used.
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null)
	{
          components.Dispose();
	}
      }

      base.Dispose( disposing );
    }

    // Initialize form components
    private void InitializeComponent()
    {
      this.Text = "Card Display";
      this.BackColor = Color.Green;
      this.Size = new Size( 340, 445 );
    }

    // The main entry point for the application.
    [STAThread]
    static void Main() 
    {
      Application.Run( new CardDisplay() );
    }

    // Override Form's OnPaint method
    protected override void OnPaint( PaintEventArgs e )
    {
      Graphics x = e.Graphics;

      hdc = x.GetHdc();
      x.ReleaseHdc(hdc);
      Draw();
    }

    // Draw card
    private void Draw()
    {
      /* First Row */
      cardHandle.drawCard( hdc, 10,  10, (int)eBACK.CROSSHATCH, 1, 207 );
      cardHandle.drawCardBack( hdc, 90,  10,  eBACK.WEAVE1 );
      cardHandle.drawCardBack( hdc, 170, 10,  eBACK.WEAVE2 );
      cardHandle.drawCardBack( hdc, 250, 10,  eBACK.ROBOT );

      /* Second Row */
      cardHandle.drawCardBack( hdc, 10,  110, eBACK.FLOWERS );
      cardHandle.drawCardBack( hdc, 90,  110, eBACK.VINE1 );
      cardHandle.drawCardBack( hdc, 170, 110, eBACK.VINE2 );
      cardHandle.drawCardBack( hdc, 250, 110, eBACK.FISH1 );

      /* Third Row */
      cardHandle.drawCardBack( hdc, 10,  210, eBACK.FISH2 );
      cardHandle.drawCardBack( hdc, 90,  210, eBACK.SHELLS );
      cardHandle.drawCardBack( hdc, 170, 210, eBACK.CASTLE );
      cardHandle.drawCardBack( hdc, 250, 210, eBACK.ISLAND );

      /* Fourth Row */
      cardHandle.drawCardBack( hdc, 10,  310, eBACK.CARDHAND );
      cardHandle.drawCardBack( hdc, 90,  310, eBACK.UNUSED );
      cardHandle.drawCardBack( hdc, 170, 310, eBACK.THE_X );
      cardHandle.drawCardBack( hdc, 250, 310, eBACK.THE_O );
    }
  }
}
