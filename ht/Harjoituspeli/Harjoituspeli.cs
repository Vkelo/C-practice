using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;

/// <summary>
/// @author Anton Kelo
/// @10.11.2021
/// peli, jossa amerikkalaiset jalkapallot saalistavat eurooppalaista jalkapalloa
/// Pelaaja yrittää syödä amerikkalaisia jalkapalloja ennen kuin ne saavuttavat eurooppalaisen jalkapallon
/// </summary>
public class Harjoituspeli : PhysicsGame
{
    readonly IntMeter pisteet = new(0);
    readonly IntMeter jalkapallonterveys = new(3);
    Vector nopeusYlos = new(0, 150);
    Vector nopeusAlas = new(0, -150);
    Vector nopeusOikealle = new(150, 0);
    Vector nopeusVasemmalle = new(-150, 0);

    PhysicsObject jalkapallo;
    PhysicsObject pelaaja;
    PhysicsObject vihu1;
    PhysicsObject vihu2;
    PhysicsObject vihu3;


    public override void Begin()
    {
        List<Label> valikonKohdat;

        Label otsikko = new("Tervetuloa"); 
        otsikko.Y = 150; 
        otsikko.Font = new Font(40, true);
        Add(otsikko);

        valikonKohdat = new List<Label>();

        Label kohta1 = new("Aloita uusi peli");  
        kohta1.Position = new Vector(0, 50);
        kohta1.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta1.Font.Size = 50;
        valikonKohdat.Add(kohta1);  

        Label kohta2 = new("Parhaat pisteet");
        kohta2.Position = new Vector(0, 0);
        kohta2.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta2.Font.Size = 40;
        valikonKohdat.Add(kohta2);

        Label kohta3 = new("Lopeta Peli");
        kohta3.Position = new Vector(0, -40);
        kohta3.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta3.Font.Size = 30;
        valikonKohdat.Add(kohta3);

        //Label kohta4 = new("Lopeta Peli");
        //kohta4.Position = new Vector(0, -80);
        //valikonKohdat.Add(kohta4);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }

        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        //Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);
        //TODO:Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, Ohjeet, null);

        Mouse.ListenOn(kohta1, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, true);
        Mouse.ListenOn(kohta1, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, false);

        Mouse.ListenOn(kohta2, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, true);
        Mouse.ListenOn(kohta2, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, false);

        Mouse.ListenOn(kohta3, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, true);
        Mouse.ListenOn(kohta3, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, false);

        //Mouse.ListenOn(kohta4, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, true);
        //Mouse.ListenOn(kohta4, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, false);

        void ValikossaLiikkuminen(Label kohta, bool paalla)
        {
            if (paalla)
            {
                kohta.TextColor = Color.Red;
            }
            else
            {
                kohta.TextColor = Color.Black;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void AloitaPeli()
    {
        ClearGameObjects();
        ClearControls();
        LuoKentta();
        AsetaOhjaimet();
        AddCollisionHandler(pelaaja, vihu1, CollisionHandler.DestroyTarget);
        AddCollisionHandler(pelaaja, vihu2, CollisionHandler.DestroyTarget);
        AddCollisionHandler(pelaaja, vihu3, CollisionHandler.DestroyTarget);

        AddCollisionHandler(jalkapallo, vihu1, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));
        AddCollisionHandler(jalkapallo, vihu1, JalkapalloHäviää);

        AddCollisionHandler(jalkapallo, vihu2, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));
        AddCollisionHandler(jalkapallo, vihu2, JalkapalloHäviää);

        AddCollisionHandler(jalkapallo, vihu3, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));
        AddCollisionHandler(jalkapallo, vihu3, JalkapalloHäviää);

        LuoPistelaskuri();
        LuoElamaLaskuri();

        void JalkapalloHäviää(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (jalkapallonterveys == 0)
            {
                jalkapallo.Destroy();
                pelaaja.Destroy();

                Timer.SingleShot(2.0, PeliLoppui); 
            }
        }
    }

    void PeliLoppui()
    {
        ClearAll();

        List<Label> valikonKohdat;

        Label otsikko = new("Hävisit :((");
        otsikko.Y = 150;
        otsikko.Font = new Font(40, true); 
        Add(otsikko);

        valikonKohdat = new List<Label>();

        //Label kohta1 = pisteet;

        Label kohta2 = new("Aloita uusi peli");
        kohta2.Position = new Vector(0, 50);
        kohta2.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta2.Font.Size = 50;
        valikonKohdat.Add(kohta2);

        Label kohta3 = new("Parhaat pisteet");
        kohta3.Position = new Vector(0, 0);
        kohta3.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta3.Font.Size = 40;
        valikonKohdat.Add(kohta3);

        Label kohta4 = new("Lopeta Peli");
        kohta4.Position = new Vector(0, -40);
        kohta4.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta4.Font.Size = 30;
        valikonKohdat.Add(kohta4);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }

        //TODO: Hiirikuuntelijat
    }

    /// <summary>
    /// 
    /// </summary>
    void LuoKentta()
    {

        /// <summary>
        /// 
        /// </summary>
        TileMap ruudut = TileMap.FromLevelAsset("kentta1.txt");
        ruudut.SetTileMethod('*', LuoSeina);
        ruudut.Execute(20.0, 20.0);
        Level.CreateBorders(0.0, false);
        Level.Background.Image = LoadImage("grass.jpeg");
        Level.Background.TileToLevel();
        Camera.ZoomToLevel();

        /// <summary>
        /// Luodaan pelin keskustaan jalkapallo
        /// </summary>
        jalkapallo = LuoJalkapallo(0.0, 0.0);

        /// <summary>
        /// Luodaan pelin päähenkilö, joka metsästää amerikkalaisia jalkapalloja
        /// </summary>
        LuoPelaaja(Level.Right - 50.0, 0.0);

        /// <summary>
        /// Luodaan viholliset kolme kertaa
        /// </summary>
        ///


        vihu1 = LuoVihu(Level.Right - 45.0, 150.0);
        PathFollowerBrain polkuaivo = new PathFollowerBrain(30);
        vihu1.Brain = polkuaivo;
        polkuaivo.Active = true;
        List<Vector> polku = new List<Vector>
        {
        new Vector(65.0, 150.0),
        new Vector(65.0, 10),
        new Vector(0.0, 0.0)
        };
        polkuaivo.Path = polku;

        vihu2 = LuoVihu(-210, 150.0);
        PathFollowerBrain polkuaivo2 = new PathFollowerBrain(30);
        vihu2.Brain = polkuaivo2;
        polkuaivo2.Active = true;
        List<Vector> polku2 = new List<Vector>
        {
        new Vector(-210.0, 40.0),
        new Vector(-50.0, 40.0),
        new Vector(0.0, 0.0)
        };
        polkuaivo2.Path = polku2;

        vihu3 = LuoVihu(-210.0, -100.0);
        PathFollowerBrain polkuaivo3 = new PathFollowerBrain(30);
        vihu3.Brain = polkuaivo3;
        polkuaivo3.Active = true;
        List<Vector> polku3 = new List<Vector>
        {
        new Vector(-210.0, 15.0),
        new Vector(-100.0, 15.0),
        new Vector(-50.0, -40.0),
        new Vector(0.0, 0.0)
        };
        polkuaivo3.Path = polku3;

    }

    /// <summary>
    /// Luodaan pelissä liikuteltava pelaaja
    /// </summary>
    /// <param name="x">Leveys</param>
    /// <param name="y">Korkeus</param>
    /// <returns>pelaajan pääohjelmaan</returns>
    PhysicsObject LuoPelaaja(double x, double y)
    {
        pelaaja = new(15.0, 15.0);
        pelaaja.Shape = Shape.Rectangle;
        pelaaja.X = x;
        pelaaja.Y = y;
        pelaaja.Restitution = 0.3;
        pelaaja.CanRotate = false;
        pelaaja.Tag = "pelaaja";      
        //pelaaja.Image = LoadImage("");
        Add(pelaaja);
        return pelaaja;
    }

        /// <summary>
        /// Luodaan kenttään fysiikkaobjekti joka luo sokkelon
        /// </summary>
        /// <param name="paikka">sijainti kentällä</param>
        /// <param name="x">leveys</param>
        /// <param name="y">korkeus</param>
    void LuoSeina(Vector paikka, double x, double y)
    {
        PhysicsObject seina = PhysicsObject.CreateStaticObject(x, y);
        seina.Position = paikka;
        seina.Shape = Shape.FromString("Rectangle");
        seina.Color = Color.Black;
        this.Add(seina);
    }
    /// <summary>
    /// Luodaan pohja vihollisille jotka metsästävät jalkapalloa.
    /// </summary>
    /// <param name="x">leveys</param>
    /// <param name="y">korkeus</param>
    /// <returns></returns>
    ///

     PhysicsObject LuoVihu(double x, double y)
     {
         vihollinen vihu = new(15.0, 15.0);
         vihu.Shape = Shape.Diamond;
         vihu.X = x;
         vihu.Y = y;
         vihu.Restitution = 0.3;
         vihu.Image = LoadImage("amerikkalainen.png");
         this.Add(vihu);
         return vihu;
     }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    PhysicsObject LuoJalkapallo(double x, double y)
    {
        pallo jalkapallo = new(30.0, 30.0, 3)
        {
            Shape = Shape.Ellipse, X = x, Y = y
        };
        jalkapallo.MakeStatic();
        jalkapallo.Restitution = 0.0;
        jalkapallo.Image = LoadImage("eurooppa.png");
        this.Add(jalkapallo);
        if (jalkapallonterveys == 0)
            jalkapallo.Destroy();
        return jalkapallo;
    }

    /// <summary>
    /// Luodaan laskuri siitä, kuinka monta elämää jalkapallolla on jäljellä
    /// </summary>
    void LuoElamaLaskuri()
    {
        Label elamanaytto = new();
        elamanaytto.X = Screen.Left + 350;
        elamanaytto.Y = Screen.Top - 50;
        elamanaytto.TextColor = Color.Black;
        elamanaytto.Color = Color.White;
        elamanaytto.Title = "elämät = ";
        elamanaytto.BindTo(jalkapallonterveys);
        Add(elamanaytto);
    }

    /// <summary>
    /// Luodaan pistelaskuri siitä, kuinka monta vihua pelaaja on syönyt.
    /// </summary>
    void LuoPistelaskuri()
    {
        Label pistenaytto = new();
        pistenaytto.X = Screen.Left + 150;
        pistenaytto.Y = Screen.Top - 50;
        pistenaytto.TextColor = Color.Black;
        pistenaytto.Color = Color.White;
        pistenaytto.Title = "pisteet = ";
        pistenaytto.BindTo(pisteet);
        Add(pistenaytto);
    }
    /// <summary>
    /// Luodaan ohjaimet yhdelle pelaajalle
    /// </summary>
    private void AsetaOhjaimet()
    {
        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Liikuta Pelaaja ylös", pelaaja, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, pelaaja, Vector.Zero);

        Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Liikuta pelaaja vasen", pelaaja, nopeusVasemmalle);
        Keyboard.Listen(Key.Left, ButtonState.Released, AsetaNopeus, null, pelaaja, Vector.Zero);

        Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Liikuta pelaaja oikea", pelaaja, nopeusOikealle);
        Keyboard.Listen(Key.Right, ButtonState.Released, AsetaNopeus, null, pelaaja, Vector.Zero);

        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Liikuta pelaaja alas", pelaaja, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, pelaaja, Vector.Zero);


        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pelaaja1"></param>
    /// <param name="nopeus"></param>
    void AsetaNopeus(PhysicsObject pelaaja1, Vector nopeus)
    {
        pelaaja1.Velocity = nopeus;
    }


}
/// <summary>
/// 
/// </summary>
class pallo : PhysicsObject
{
    private IntMeter elamalaskuri = new IntMeter(3, 0, 3);
    public IntMeter Elamalaskuri { get { return elamalaskuri; } }

    public pallo (double leveys, double korkeus, int elamia)
        : base(leveys, korkeus)
    {
        elamia = 3;
        elamalaskuri.LowerLimit += delegate { this.Destroy(); };
    }
}

class vihollinen : PhysicsObject
{
    public vihollinen (double leveys, double korkeus)
        : base(leveys, korkeus)
    {
        Timer.CreateAndStart(6, LuoUusiVihu);
    }

    void LuoUusiVihu()
    {
        vihollinen vihu = new(15.0, 15.0);
        vihu.Shape = Shape.Diamond;
        vihu.X = 30;
        vihu.Y = 30;
        vihu.Restitution = 0.3;
        //vihu.Image = LoadImage("amerikkalainen.png");

        //this.Add(vihu);
    }
}