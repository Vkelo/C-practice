using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Widgets;

/// <summary>
/// @author Anton Kelo
/// @29.11.2021
/// peli, jossa amerikkalaiset jalkapallot saalistavat eurooppalaista jalkapalloa
/// Pelaaja yrittää syödä amerikkalaisia jalkapalloja ennen kuin ne saavuttavat eurooppalaisen jalkapallon
/// </summary>
public class Harjoituspeli : PhysicsGame
{

    Vector nopeusYlos = new(0, 150);
    Vector nopeusAlas = new(0, -150);
    Vector nopeusOikealle = new(150, 0);
    Vector nopeusVasemmalle = new(-150, 0);

    PhysicsObject jalkapallo;
    PhysicsObject pelaaja;
    PhysicsObject vihu1;
    PhysicsObject vihu2;
    PhysicsObject vihu3;
    PhysicsObject vihu4;
    PhysicsObject vihu5;
    PhysicsObject vihu6;
    PhysicsObject vihu7;
    PhysicsObject vihu8;

    EasyHighScore topLista = new EasyHighScore();

    IntMeter pisteet = new(0);
    IntMeter jalkapallonterveys = new(3);


    public override void Begin()
    {
        ClearAll();

        List<Label> valikonKohdat;

        Label otsikko = new("Tervetuloa");
        otsikko.Y = 150;
        otsikko.Font = new Font(20, true);
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

        Label kohta4 = new("ohjeet");
        kohta4.Position = new Vector(0, -80);
        kohta4.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        valikonKohdat.Add(kohta4);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }


        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);
        Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, Ohjeet, null);

        Mouse.ListenOn(kohta1, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, true);
        Mouse.ListenOn(kohta1, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta1, false);

        Mouse.ListenOn(kohta2, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, true);
        Mouse.ListenOn(kohta2, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, false);

        Mouse.ListenOn(kohta3, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, true);
        Mouse.ListenOn(kohta3, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, false);

        Mouse.ListenOn(kohta4, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, true);
        Mouse.ListenOn(kohta4, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, false);

        static void ValikossaLiikkuminen(Label kohta, bool paalla)
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

        void ParhaatPisteet()
        {
            ClearAll();
            topLista.Show();
            Timer.SingleShot(5, Begin);
        }
    }



    /// <summary>
    /// Näyttö, jossa kerrotaan ohjeet ja annetaan muutamia asetuksia
    /// </summary>
    private void Ohjeet()
    {
        ClearAll();

        List<Label> valikonKohdat;

        Label otsikko = new("Ohjeet");
        otsikko.Y = 150;
        otsikko.Font = new Font(40, true);
        Add(otsikko);

        valikonKohdat = new List<Label>();

        Label kohta1 = new("Tehtäväsi on saalistaa mahdollisimman monta amerikkalaista jalkapalloa pelihahmoa käyttäämällä.");
        kohta1.Position = new Vector(0, 50);
        kohta1.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta1.Font.Size = 20;
        valikonKohdat.Add(kohta1);

        Label kohta2 = new("Äänenvoimakkuus");
        kohta2.Position = new Vector(0, 0);
        kohta2.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta2.Font.Size = 30;
        valikonKohdat.Add(kohta2);

        Label kohta3 = new("Skinit");
        kohta3.Position = new Vector(0, -40);
        kohta3.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta3.Font.Size = 30;
        valikonKohdat.Add(kohta3);

        Label kohta4 = new("Näppäimet");
        kohta4.Position = new Vector(0, -80);
        kohta4.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta4.Font.Size = 30;
        valikonKohdat.Add(kohta4);

        Label kohta5 = new("Takaisin");
        kohta5.Position = new Vector(0, -120);
        kohta5.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta5.Font.Size = 30;
        valikonKohdat.Add(kohta5);

        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }

        Mouse.ListenOn(kohta2, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, true);
        Mouse.ListenOn(kohta2, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, false);

        Mouse.ListenOn(kohta3, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, true);
        Mouse.ListenOn(kohta3, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, false);

        Mouse.ListenOn(kohta4, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, true);
        Mouse.ListenOn(kohta4, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, false);

        Mouse.ListenOn(kohta5, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta5, true);
        Mouse.ListenOn(kohta5, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta5, false);

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


        Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, Aanenvoimakkuus, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, Skinit, null);
        //Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, Näppäimet, null);
        Mouse.ListenOn(kohta5, MouseButton.Left, ButtonState.Pressed, Begin, null);


        void Skinit()
        {
            ClearAll();


            List<Label> SkiniKohdat;

            Label otsikko2 = new("SKINIT");
            otsikko.Y = 150;
            otsikko.Font = new Font(40, true);
            Add(otsikko);

            SkiniKohdat = new List<Label>();

            Label kohta6 = new("Leopardi");
            kohta6.Position = new Vector(0, 40);
            kohta6.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
            kohta6.Font.Size = 30;
            SkiniKohdat.Add(kohta6);

            Label kohta7 = new("Markku Kanerva");
            kohta7.Position = new Vector(0, 0);
            kohta7.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
            kohta7.Font.Size = 30;
            SkiniKohdat.Add(kohta7);

            Label kohta8 = new("Seepra");
            kohta8.Position = new Vector(0, -40);
            kohta8.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
            kohta8.Font.Size = 30;
            SkiniKohdat.Add(kohta8);

            Label kohta9 = new("JYU");
            kohta9.Position = new Vector(0, -80);
            kohta9.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
            kohta9.Font.Size = 30;
            SkiniKohdat.Add(kohta9);

            Label kohta10 = new("Takaisin");
            kohta10.Position = new Vector(0, -150);
            kohta10.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
            kohta10.Font.Size = 30;
            SkiniKohdat.Add(kohta10);

            Label kohta11 = new Label
            {
                Position = new Vector(100, 40),
                Height = 25,
                Width = 25,
                Image = LoadImage("eurooppa.png")
            };
            SkiniKohdat.Add(kohta11);

            foreach (Label Skinikohta in SkiniKohdat)
            {
                Add(Skinikohta);
            }
            Mouse.ListenOn(kohta10, MouseButton.Left, ButtonState.Pressed, Ohjeet, null);
        }

        void Aanenvoimakkuus()
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

        void SaadaVoimakkuutta(double vanhaArvo, double uusiArvo)
        {
            MasterVolume = 0 + uusiArvo;
        }

    }


    /// <summary>
    /// Luo tason 1 tapahtumakäskyt, kutsuu aliohjelmia jotka luo kentän, ohjaimet ja viholliset.
    /// Määrittelee voittaako pelaaja vai ei.
    /// </summary>
    private void AloitaPeli()
    {
        ClearAll();

        IntMeter VihunTerveys = new(3);

        LuoKentta();
        AsetaOhjaimet();


        Taso1Vihut();

        AddCollisionHandler(pelaaja, vihu1, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu1, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu1, PelaajaVoittaa);
        AddCollisionHandler(pelaaja, vihu2, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu2, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu2, PelaajaVoittaa);
        AddCollisionHandler(pelaaja, vihu3, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu3, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu3, PelaajaVoittaa);
        AddCollisionHandler(pelaaja, vihu4, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu4, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu4, PelaajaVoittaa);

        AddCollisionHandler(jalkapallo, vihu1, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu1, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu2, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu2, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu3, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu3, JalkapalloHäviää);
        AddCollisionHandler(jalkapallo, vihu4, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu4, JalkapalloHäviää);

        LuoPistelaskuri(pisteet);
        LuoElamaLaskuri(jalkapallonterveys);

        void JalkapalloHäviää(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (jalkapallonterveys == 0)
            {
                jalkapallo.Destroy();
                pelaaja.Destroy();

                Timer.SingleShot(10.0, Hävisit);
                topLista.EnterAndShow(pisteet.Value);
            }

        }


        void PelaajaVoittaa(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (VihunTerveys == 0)
            {
                vihu1.Destroy();
                vihu2.Destroy();
                vihu3.Destroy();
                vihu4.Destroy();
                Timer.SingleShot(1.0, Voitit);
            }
        }

        Timer.SingleShot(20, Voitit);
    }


    /// <summary>
    /// Luo lopetusnäytön, on kutsuttu kun pelaaja häviää
    /// </summary>
    private void Hävisit()
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


        Mouse.ListenOn(kohta2, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, true);
        Mouse.ListenOn(kohta2, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, false);

        //Mouse.ListenOn(kohta3, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, true);
        //Mouse.ListenOn(kohta3, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, false);

        Mouse.ListenOn(kohta4, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, true);
        Mouse.ListenOn(kohta4, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, false);

        static void ValikossaLiikkuminen(Label kohta, bool paalla)
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


        Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        //Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, ParhaatPisteet, null);
        Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);

    }


    /// <summary>
    /// Luo voitit-näytön jos pelaaja pääsee eteenpäin
    /// </summary>
    private void Voitit()
    {
        ClearGameObjects();


        List<Label> valikonKohdat;

        Label otsikko = new("Voitit :))");
        otsikko.Y = 150;
        otsikko.Font = new Font(40, true);
        Add(otsikko);

        valikonKohdat = new List<Label>();

        Label kohta2 = new("Jatka seuraavaan tasoon");
        kohta2.Position = new Vector(0, 50);
        kohta2.Font = LoadFont("HVD_Comic_Serif_Pro.otf");
        kohta2.Font.Size = 50;
        valikonKohdat.Add(kohta2);

        Label kohta3 = new("Aloita alusta");
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


        Mouse.ListenOn(kohta2, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, true);
        Mouse.ListenOn(kohta2, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta2, false);

        Mouse.ListenOn(kohta3, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, true);
        Mouse.ListenOn(kohta3, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta3, false);

        Mouse.ListenOn(kohta4, HoverState.Enter, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, true);
        Mouse.ListenOn(kohta4, HoverState.Exit, MouseButton.None, ButtonState.Irrelevant, ValikossaLiikkuminen, null, kohta4, false);

        static void ValikossaLiikkuminen(Label kohta, bool paalla)
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

        Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, Taso2, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, AloitaPeli, null);
        Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, ConfirmExit, null);

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
        vihu1 = LuoVihu(Level.Right - 45.0, 150.0);
        PathFollowerBrain polkuaivo = new PathFollowerBrain(30);
        vihu1.Brain = polkuaivo;
        polkuaivo.Active = true;
        List<Vector> polku = new()
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
        List<Vector> polku2 = new()
        {
            new Vector(-210.0, 40.0),
            new Vector(-50.0, 40.0),
            new Vector(0.0, 0.0)
        };
        polkuaivo2.Path = polku2;


        vihu3 = LuoVihu(-220.0, -150.0);
        PathFollowerBrain polkuaivo3 = new(30);
        vihu3.Brain = polkuaivo3;
        polkuaivo3.Active = true;
        List<Vector> polku3 = new()
        {
            new Vector(-210.0, 17.0),
            new Vector(-85.0, 15.0),
            new Vector(-85.0, -40.0),
            new Vector(-50.0, -40.0),
            new Vector(-40.0, 0.0),
            new Vector(0.0, 0.0)
        };
        polkuaivo3.Path = polku3;

        vihu4 = LuoVihu(210.0, -150.0);
        PathFollowerBrain polkuaivo4 = new(30);
        vihu4.Brain = polkuaivo4;
        polkuaivo4.Active = true;
        List<Vector> polku4 = new()
        {
            new Vector(82.0, -150.0),
            new Vector(82.0, -30.0),
            new Vector(50.0, -30.0),
            new Vector(50.0, 0.0),
            new Vector(0.0, 0.0)
        };
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
        vihu.Restitution = 0.3;
        vihu.Image = LoadImage("amerikkalainen.png");
        this.Add(vihu);
        return vihu;
    }


    /// <summary>
    /// Luodaan pelin keskelle jalkapallo, jota vihut yrittävät metsästää
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PhysicsObject LuoJalkapallo(double x, double y)
    {
        Pallo jalkapallo = new(30.0, 30.0, 3)
        {
            Shape = Shape.Ellipse, X = x, Y = y
        };
        jalkapallo.MakeStatic();
        jalkapallo.Restitution = 0.0;
        jalkapallo.Image = LoadImage("eurooppa.png");
        this.Add(jalkapallo);
        //if (jalkapallonterveys == 0)
        //    jalkapallo.Destroy();
        return jalkapallo;
    }


    /// <summary>
    /// Luodaan laskuri siitä, kuinka monta elämää jalkapallolla on jäljellä
    /// </summary>
    Label LuoElamaLaskuri(IntMeter e)
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
    private void AsetaNopeus(PhysicsObject pelaaja1, Vector nopeus)
    {
        pelaaja1.Velocity = nopeus;
    }


    /// <summary>
    /// luodaan taso 2, käytetään osittain tason 1 aliohjelmia
    /// </summary>
    private void Taso2()
    {
        ClearAll();

        IntMeter VihunTerveys = new(3);
        LuoKentta2();

        jalkapallo = LuoJalkapallo(0.0, 0.0);
        LuoPelaaja(50.0, 0.0);
        AsetaOhjaimet();

        LuoElamaLaskuri(jalkapallonterveys);
        LuoPistelaskuri(pisteet);
        Taso2Vihut();

        AddCollisionHandler(pelaaja, vihu5, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu5, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu5, PelaajaVoittaa2);
        AddCollisionHandler(pelaaja, vihu6, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu6, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu6, PelaajaVoittaa2);
        AddCollisionHandler(pelaaja, vihu7, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu7, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu7, PelaajaVoittaa2);
        AddCollisionHandler(pelaaja, vihu8, CollisionHandler.AddMeterValue(pisteet, +1));AddCollisionHandler(pelaaja, vihu8, CollisionHandler.AddMeterValue(VihunTerveys, -1));AddCollisionHandler(pelaaja, vihu8, PelaajaVoittaa2);

        AddCollisionHandler(jalkapallo, vihu5, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu5, JalkapalloHäviää2);
        AddCollisionHandler(jalkapallo, vihu6, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu6, JalkapalloHäviää2);
        AddCollisionHandler(jalkapallo, vihu7, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu7, JalkapalloHäviää2);
        AddCollisionHandler(jalkapallo, vihu8, CollisionHandler.AddMeterValue(jalkapallonterveys, -1));AddCollisionHandler(jalkapallo, vihu8, JalkapalloHäviää2);

        void JalkapalloHäviää2(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (jalkapallonterveys == 0)
            {
                jalkapallo.Destroy();
                pelaaja.Destroy();

                Timer.SingleShot(10.0, Hävisit);
                topLista.EnterAndShow(pisteet.Value);
            }

        }

        void PelaajaVoittaa2(PhysicsObject tormaaja, PhysicsObject kohde)
        {
            kohde.Destroy();
            if (VihunTerveys == 0)
            {
                vihu1.Destroy();
                vihu2.Destroy();
                vihu3.Destroy();
                vihu4.Destroy();
                Timer.SingleShot(1.0, Voitit);
            }
        }

        Timer.SingleShot(10, Voitit);
    }

    private void Taso2Vihut()
    {
        vihu5 = LuoVihu(0.0, 150.0);
        PathFollowerBrain polkuaivo5 = new PathFollowerBrain(30);
        vihu5.Brain = polkuaivo5;
        polkuaivo5.Active = true;
        List<Vector> polku = new()
        {
            new Vector(0.0, 0.0),
            new Vector(0.0, 10),
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



/// <summary>
/// Luodaan oma luokka jalkapallolle
/// </summary>
class Pallo : PhysicsObject
{
    private IntMeter elamalaskuri = new IntMeter(3, 0, 3);
    public IntMeter Elamalaskuri { get { return elamalaskuri; } }

    public Pallo (double leveys, double korkeus, int elamia)
        : base(leveys, korkeus)
    {
        elamia = 3;
        elamalaskuri.LowerLimit += delegate { this.Destroy(); };
    }
}

