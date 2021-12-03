using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Widgets;

/// <summary>
/// @author Anton Kelo
/// @30.11.2021
/// peli, jossa amerikkalaiset jalkapallot saalistavat eurooppalaista jalkapalloa
/// Pelaaja yrittää syödä amerikkalaisia jalkapalloja ennen kuin ne saavuttavat eurooppalaisen jalkapallon
/// </summary>
public class SavingTheBall : PhysicsGame
{

    Font fontti = LoadFont("HVD_Comic_Serif_Pro.otf");
    Font fontti2 = Font.Default;

    private PhysicsObject jalkapallo;
    private PhysicsObject pelaaja;
    private PhysicsObject vihu1;
    private PhysicsObject vihu2;
    private PhysicsObject vihu3;
    private PhysicsObject vihu4;

    EasyHighScore topLista = new EasyHighScore();

    private IntMeter pisteet = new(0);
    private IntMeter Jalkapallonterveys = new(3);
    private IntMeter VihunTerveys = new(3);

    /// <summary>
    /// Aloittaa pelin
    /// </summary>
    public override void Begin()
    {
        ClearAll();
        AloitusValikko();
    }


    /// <summary>
    /// Luo aloitusvalikon vaihtoehdot
    /// </summary>
    /// <param name="teksti">Annettu teksti</param>
    /// <param name="paikka">Tekstin sijainti</param>
    /// <param name="fontti">Ladattu fontti</param>
    /// <returns></returns>
    private Label Valikko(string teksti, Vector paikka, Font fontti)
    {
        Label valikonKohta = new(teksti);
        valikonKohta.Position = paikka;
        valikonKohta.Font = fontti;
        valikonKohta.Font.Size = 40;
        Add(valikonKohta);
        return valikonKohta;
    }


    /// <summary>
    /// Maalaa tekstin punaiseksi, kun hiiren vie päälle
    /// </summary>
    /// <param name="kohta">tekstin sijainti</param>
    /// <param name="paalla">onko hiiri päällä vai ei</param>
    private void ValikossaLiikkuminen(Label kohta, bool paalla)
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


    /// <summary>
    /// Luo aloitusvalikon
    /// </summary>
    private void AloitusValikko()
    {
        Vector paikka1 = new(0, 150);
        Vector paikka2 = new(0, 50);
        Vector paikka3 = new(0, 0);
        Vector paikka4 = new(0, -50);
        Vector paikka5 = new(0, -100);

        List<Label> valikonKohdat;
        valikonKohdat = new List<Label>();

        Label valikonKohta1 = Valikko("Tervetuloa", paikka1, fontti);
        valikonKohdat.Add(valikonKohta1);
        Label valikonKohta2 = Valikko("Aloita uusi peli", paikka2, fontti);
        valikonKohdat.Add(valikonKohta2);
        Label valikonKohta3 = Valikko("Parhaat pisteet", paikka3, fontti);
        valikonKohdat.Add(valikonKohta3);
        Label valikonKohta4 = Valikko("Ohjeet", paikka4, fontti);
        valikonKohdat.Add(valikonKohta4);
        Label valikonKohta5 = Valikko("Lopeta", paikka5, fontti);
        valikonKohdat.Add(valikonKohta5);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Mouse.ListenOn(valikonKohta, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, true);
            Mouse.ListenOn(valikonKohta, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, false);
        }

        Mouse.ListenOn(valikonKohta2, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        Mouse.ListenOn(valikonKohta3, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
        Mouse.ListenOn(valikonKohta4, MouseButton.Left, ButtonState.Pressed, Ohjeet, null);
        Mouse.ListenOn(valikonKohta5, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);
    }


    /// <summary>
    /// Näyttää ajan saatossa saadut parhaat pisteet
    /// </summary>
    private void ParhaatPisteet()
    {
        ClearAll();
        topLista.Show();
        Timer.SingleShot(5, Begin);
    }


    /// <summary>
    /// Näyttö, jossa kerrotaan ohjeet ja annetaan muutamia asetuksia
    /// </summary>
    private void Ohjeet()
    {
        ClearAll();

        Vector paikka0 = new(0, 200);
        Vector paikka1 = new(0, 100);
        Vector paikka2 = new(0, 50);
        Vector paikka3 = new(0, 0);
        Vector paikka4 = new(0, -50);
        Vector paikka5 = new(0, -100);

        List<Label> valikonKohdat;
        valikonKohdat = new List<Label>();
        Label otsikko = Valikko("Ohjeet", paikka0, fontti2);
        Label ohjeet1 = Valikko("Saalista amerikkalaisia jalkapalloja pelaajalla", paikka1, fontti2);
        Label ohjeet2 = Valikko("Äänenvoimakkuus", paikka2, fontti);
        valikonKohdat.Add(ohjeet2);
        Label ohjeet3 = Valikko("Skinit", paikka3, fontti);
        valikonKohdat.Add(ohjeet3);
        Label ohjeet4 = Valikko("Näppäimet", paikka4, fontti);
        valikonKohdat.Add(ohjeet4);
        Label ohjeet5 = Valikko("Takaisin", paikka5, fontti);
        valikonKohdat.Add(ohjeet5);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Mouse.ListenOn(valikonKohta, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, true);
            Mouse.ListenOn(valikonKohta, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, false);
        }

        Mouse.ListenOn(ohjeet2, MouseButton.Left, ButtonState.Pressed, Aanenvoimakkuus, null);
        Mouse.ListenOn(ohjeet3, MouseButton.Left, ButtonState.Pressed, Skinit, null);
        //Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, Näppäimet, null);
        Mouse.ListenOn(ohjeet5, MouseButton.Left, ButtonState.Pressed, Begin, null);
    }


    /// <summary>
    /// Valikko, missä voi valita pelaajalle skinin
    /// </summary>
    void Skinit()
    {
        ClearAll();

        Vector paikka1 = new(0, 100);
        Vector paikka2 = new(0, 50);
        Vector paikka3 = new(0, 0);
        Vector paikka4 = new(0, -50);
        Vector paikka5 = new(0, -100);

        Label skinit1 = Valikko("Zebra", paikka1, fontti);
        Label skinit2 = Valikko("Leopard", paikka2, fontti);
        Label skinit3 = Valikko("RIVE", paikka3, fontti);
        Label skinit4 = Valikko("JYU", paikka4, fontti);
        Label skinit5 = Valikko("Takaisin", paikka5, fontti);

        Mouse.ListenOn(skinit5, MouseButton.Left, ButtonState.Pressed, Ohjeet, null);
    }


    /// <summary>
    /// Äänenvoimakkuussäädin muuttaa master volumen voimakkuutta
    /// </summary>
    private void Aanenvoimakkuus()
    {
        DoubleMeter voimakkuus = new(1, 0, 1);
        voimakkuus.Changed += SaadaVoimakkuutta;

        Slider liukusaadin = new(200, 20);
        liukusaadin.BindTo(voimakkuus);
        liukusaadin.X = 250;
        liukusaadin.Y = 0;

        liukusaadin.Color = Color.Pink;
        liukusaadin.Knob.Color = Color.Black;
        liukusaadin.Track.Color = Color.Blue;
        liukusaadin.BorderColor = Color.Red;

        Add(liukusaadin);
    }


    /// <summary>
    /// Muuttaa itse maste volumea
    /// </summary>
    /// <param name="vanhaArvo">Vanha äänenvoimakkuus</param>
    /// <param name="uusiArvo">Uusi äänenvoimakkuus</param>
    public void SaadaVoimakkuutta(double vanhaArvo, double uusiArvo)
    {
        MasterVolume = 0 + uusiArvo;
    }


    /// <summary>
    /// Luo lopetusnäytön, on kutsuttu kun pelaaja häviää
    /// </summary>
    private void Hävisit()
    {
        ClearAll();

        Vector paikka1 = new(0, 150);
        Vector paikka2 = new(0, 50);
        Vector paikka3 = new(0, 0);
        Vector paikka4 = new(0, -50);

        List<Label> valikonKohdat;
        valikonKohdat = new List<Label>();
        Label Havisit1 = Valikko("Hävisit :((", paikka1, fontti);
        Label Havisit2 = Valikko("Aloita uusi peli", paikka2, fontti);
        valikonKohdat.Add(Havisit2);
        Label Havisit3 = Valikko("Parhaat pisteet", paikka3, fontti);
        valikonKohdat.Add(Havisit3);
        Label Havisit4 = Valikko("Lopeta Peli", paikka4, fontti);
        valikonKohdat.Add(Havisit4);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Mouse.ListenOn(valikonKohta, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, true);
            Mouse.ListenOn(valikonKohta, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, false);
        }

        Mouse.ListenOn(Havisit2, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        Mouse.ListenOn(Havisit3, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
        Mouse.ListenOn(Havisit4, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);
    }


    /// <summary>
    /// Luo voitit-näytön jos pelaaja pääsee eteenpäin
    /// </summary>
    private void Voitit()
    {
        ClearAll();

        Vector paikka1 = new(0, 150);
        Vector paikka2 = new(0, 50);
        Vector paikka3 = new(0, 0);
        Vector paikka4 = new(0, -50);

        List<Label> valikonKohdat;
        valikonKohdat = new List<Label>();
        Label Voitit1 = Valikko("Voitit :))", paikka1, fontti);
        Label Voitit2 = Valikko("Jatka seuraavaan tasoon", paikka2, fontti);
        valikonKohdat.Add(Voitit2);
        Label Voitit3 = Valikko("Aloita alusta", paikka3, fontti);
        valikonKohdat.Add(Voitit3);
        Label Voitit4 = Valikko("Lopeta Peli", paikka4, fontti);
        valikonKohdat.Add(Voitit4);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Mouse.ListenOn(valikonKohta, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, true);
            Mouse.ListenOn(valikonKohta, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, valikonKohta, false);
        }

        Mouse.ListenOn(Voitit2, MouseButton.Left, ButtonState.Pressed, Taso2, null);
        Mouse.ListenOn(Voitit3, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        Mouse.ListenOn(Voitit4, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);
    }


    /// <summary>
    /// Luo tason 1 tapahtumakäskyt, kutsuu aliohjelmia jotka luo kentän, ohjaimet ja viholliset.
    /// Määrittelee voittaako pelaaja vai ei.
    /// </summary>
    private void AloitaPeli()
    {
        ClearAll();
        LuoKentta();
        AsetaOhjaimet();

        pisteet = new(0);
        Taso1Vihut();

        AddCollisionHandler(jalkapallo, vihu1, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1)); AddCollisionHandler(jalkapallo, vihu1, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu2, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1)); AddCollisionHandler(jalkapallo, vihu2, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu3, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1)); AddCollisionHandler(jalkapallo, vihu3, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu4, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1)); AddCollisionHandler(jalkapallo, vihu4, JalkapalloHäviää);

        LuoPistelaskuri(pisteet);
        LuoElamaLaskuri(Jalkapallonterveys);

        void JalkapalloHäviää(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (Jalkapallonterveys == 0)
            {
                jalkapallo.Destroy();
                pelaaja.Destroy();

                Timer.SingleShot(9.0, Hävisit);
                topLista.EnterAndShow(pisteet.Value);
            }

        }

        Timer.SingleShot(25, Voitit);
    }


    /// <summary>
    /// Luo pelaajan, jalkapallon ja kentän
    /// </summary>
    private void LuoKentta()
    {

        TileMap ruudut = TileMap.FromLevelAsset("kentta1");
        ruudut.SetTileMethod('*', LuoSeina);
        ruudut.Execute(20.0, 20.0);
        Level.CreateBorders(0.0, false);
        Level.Background.Image = LoadImage("grass.jpeg");
        Level.Background.TileToLevel();
        Camera.ZoomToLevel();

        jalkapallo = LuoJalkapallo(0.0, 0.0);

        LuoPelaaja(Level.Right - 50.0, 0.0);
    }


    /// <summary>
    /// Luo viholliset tasoon yksi
    /// </summary>
    private void Taso1Vihut()
    {
        List<Vector> polku1 = new(){new Vector(65.0, 150.0),new Vector(65.0, 10),new Vector(0.0, 0.0)};
        List<Vector> polku2 = new(){new Vector(-210.0, 40.0),new Vector(-50.0, 40.0),new Vector(0.0, 0.0)};
        List<Vector> polku3 = new(){new Vector(-210.0, 17.0),new Vector(-85.0, 15.0),new Vector(-85.0, -40.0),new Vector(-50.0, -40.0),new Vector(-40.0, 0.0),new Vector(0.0, 0.0)};
        List<Vector> polku4 = new(){new Vector(82.0, -150.0),new Vector(82.0, -30.0),new Vector(50.0, -30.0),new Vector(50.0, 0.0),new Vector(0.0, 0.0)};

        vihu1 = LuoVihu(Level.Right - 45.0, 150.0);
        PathFollowerBrain polkuaivo = new PathFollowerBrain(30);
        vihu1.Brain = polkuaivo;
        polkuaivo.Active = true;
        polkuaivo.Path = polku1;

        vihu2 = LuoVihu(-210, 150.0);
        PathFollowerBrain polkuaivo2 = new PathFollowerBrain(30);
        vihu2.Brain = polkuaivo2;
        polkuaivo2.Active = true;
        polkuaivo2.Path = polku2;

        vihu3 = LuoVihu(-220.0, -150.0);
        PathFollowerBrain polkuaivo3 = new(30);
        vihu3.Brain = polkuaivo3;
        polkuaivo3.Active = true;
        polkuaivo3.Path = polku3;

        vihu4 = LuoVihu(210.0, -150.0);
        PathFollowerBrain polkuaivo4 = new(30);
        vihu4.Brain = polkuaivo4;
        polkuaivo4.Active = true;
        polkuaivo4.Path = polku4;
    }


    /// <summary>
    /// Luodaan pelissä liikuteltava pelaaja
    /// </summary>
    /// <param name="x">Leveys</param>
    /// <param name="y">Korkeus</param>
    /// <returns>pelaajan pääohjelmaan</returns>
    private PhysicsObject LuoPelaaja(double x, double y)
    {
        pelaaja = new(15.0, 15.0);
        pelaaja.Shape = Shape.Ellipse;
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
    /// Luodaan kenttään fysiikkaobjekti joka luo sokkelon seinät.
    /// </summary>
    /// <param name="paikka">sijainti kentällä</param>
    /// <param name="x">leveys</param>
    /// <param name="y">korkeus</param>
    private void LuoSeina(Vector paikka, double x, double y)
    {
        PhysicsObject seina = PhysicsObject.CreateStaticObject(x, y);
        seina.Position = paikka;
        seina.Shape = Shape.FromString("Rectangle");
        seina.Color = Color.Black;
        this.Add(seina);
    }


    /// <summary>
    /// Luodaan viholliset annettuun paikkaan
    /// </summary>
    /// <param name="x">leveys</param>
    /// <param name="y">korkeus</param>
    /// <returns></returns>
    ///
    private PhysicsObject LuoVihu(double x, double y)
    {
        PhysicsObject vihu = new(15.0, 15.0);
        vihu.Shape = Shape.Diamond;
        vihu.X = x;
        vihu.Y = y;
        vihu.Restitution = 0.5;
        AddCollisionHandler(pelaaja, vihu, CollisionHandler.AddMeterValue(pisteet, +1)); AddCollisionHandler(pelaaja, vihu, CollisionHandler.AddMeterValue(VihunTerveys, -1)); AddCollisionHandler(pelaaja, vihu, PelaajaVoittaa);
        vihu.Image = LoadImage("amerikkalainen.png");
        this.Add(vihu);
        return vihu;
    }


    /// <summary>
    /// Lopettaa pelin vihollisen elämien ollessa nolla
    /// </summary>
    /// <param name="tormaaja">Pelaaja</param>
    /// <param name="kohde">Vihu</param>
    private void PelaajaVoittaa(PhysicsObject tormaaja, PhysicsObject kohde)
    {
        kohde.Destroy();
        if (VihunTerveys == 0)
        {
            vihu1.Destroy();
            vihu2.Destroy();
            vihu3.Destroy();
            vihu4.Destroy();
            Timer.SingleShot(1.0, Voitit);
            VihunTerveys = new(3);
        }
    }


    /// <summary>
    /// Luodaan pelin keskelle jalkapallo, jota vihut yrittävät metsästää
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PhysicsObject LuoJalkapallo(double x, double y)
    {
        PhysicsObject jalkapallo = new(30.0, 30.0, Shape.Ellipse)
        {
            X = x,
            Y = y
        };
        jalkapallo.MakeStatic();
        jalkapallo.Restitution = 0.0;
        jalkapallo.Image = LoadImage("eurooppa.png");
        this.Add(jalkapallo);
        return jalkapallo;
    }


    /// <summary>
    /// Luodaan laskuri siitä, kuinka monta elämää jalkapallolla on jäljellä
    /// </summary>
    private Label LuoElamaLaskuri(IntMeter e)
    {
        Label elamanaytto = new();
        elamanaytto.X = Screen.Left + 350;
        elamanaytto.Y = Screen.Top - 50;
        elamanaytto.TextColor = Color.Black;
        elamanaytto.Color = Color.White;
        elamanaytto.Title = "elämät = ";
        elamanaytto.BindTo(e);
        Add(elamanaytto);
        return elamanaytto;
    }


    /// <summary>
    /// Luodaan pistelaskuri siitä, kuinka monta vihua pelaaja on syönyt.
    /// </summary>
    private void LuoPistelaskuri(IntMeter p)
    {
        Label pistenaytto = new();
        pistenaytto.X = Screen.Left + 150;
        pistenaytto.Y = Screen.Top - 50;
        pistenaytto.TextColor = Color.Black;
        pistenaytto.Color = Color.White;
        pistenaytto.Title = "pisteet = ";
        pistenaytto.BindTo(p);
        Add(pistenaytto);

    }


    /// <summary>
    /// Luodaan ohjaimet yhdelle pelaajalle
    /// </summary>
    private void AsetaOhjaimet()
    {
        Vector nopeusYlos = new(0, 150);
        Vector nopeusAlas = new(0, -150);
        Vector nopeusOikealle = new(150, 0);
        Vector nopeusVasemmalle = new(-150, 0);

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
    /// Asetetaan pelaajan liikenopeus
    /// </summary>
    /// <param name="pelaaja1">Pelaaja</param>
    /// <param name="nopeus">Nopeus</param>
    private void AsetaNopeus(PhysicsObject pelaaja1, Vector nopeus)
    {
        pelaaja1.Velocity = nopeus;
    }


    private PhysicsObject vihu5;
    private PhysicsObject vihu6;
    private PhysicsObject vihu7;
    private PhysicsObject vihu8;
    /// <summary>
    /// luodaan taso 2, käytetään osittain tason 1 aliohjelmia
    /// </summary>
    private void Taso2()
    {
        ClearAll();

        LuoKentta2();

        jalkapallo = LuoJalkapallo(0.0, 0.0);
        LuoPelaaja(50.0, 0.0);
        AsetaOhjaimet();

        LuoPistelaskuri(pisteet);
        LuoElamaLaskuri(Jalkapallonterveys);
        Taso2Vihut();

        AddCollisionHandler(jalkapallo, vihu5, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu5, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu6, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu6, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu7, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu7, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu8, CollisionHandler.AddMeterValue(Jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu8, JalkapalloHäviää);

        Timer.SingleShot(10, Voitit);

        void JalkapalloHäviää(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (Jalkapallonterveys == 0)
            {
                jalkapallo.Destroy();
                pelaaja.Destroy();

                Timer.SingleShot(9.0, Hävisit);
                topLista.EnterAndShow(pisteet.Value);
            }

        }
    }


    /// <summary>
    /// Luo tason 2 viholliset
    /// </summary>
    private void Taso2Vihut()
    {
        vihu5 = LuoVihu(0.0, 150.0);
        PathFollowerBrain polkuaivo5 = new PathFollowerBrain(30);
        vihu5.Brain = polkuaivo5;
        polkuaivo5.Active = true;
        List<Vector> polku = new()
        {
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0)
        };
        polkuaivo5.Path = polku;


        vihu6 = LuoVihu(0.0, -150.0);
        PathFollowerBrain polkuaivo6 = new PathFollowerBrain(30);
        vihu6.Brain = polkuaivo6;
        polkuaivo6.Active = true;
        List<Vector> polku2 = new()
        {
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0)
        };
        polkuaivo6.Path = polku2;


        vihu7 = LuoVihu(-210.0, 0.0);
        PathFollowerBrain polkuaivo7 = new(30);
        vihu7.Brain = polkuaivo7;
        polkuaivo7.Active = true;
        List<Vector> polku3 = new()
        {
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0)
        };
        polkuaivo7.Path = polku3;

        vihu8 = LuoVihu(210.0, 0.0);
        PathFollowerBrain polkuaivo8 = new(30);
        vihu8.Brain = polkuaivo8;
        polkuaivo8.Active = true;
        List<Vector> polku4 = new()
        {
            new Vector(0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0),
            new Vector(0.0, 0.0)
        };
        polkuaivo8.Path = polku4;
    }


    /// <summary>
    /// Luo tason 2 kentän
    /// </summary>
    void LuoKentta2()
    {
        TileMap ruudut2 = TileMap.FromLevelAsset("kentta2");
        ruudut2.SetTileMethod('*', LuoSeina);
        ruudut2.Execute(20.0, 20.0);
        Level.CreateBorders(0.0, false);
        Level.Background.Image = LoadImage("grass.jpeg");
        Level.Background.TileToLevel();
        Camera.ZoomToLevel();
    }
}


