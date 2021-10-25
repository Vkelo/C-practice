using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class Harjoituspeli : PhysicsGame
{
    Vector nopeusYlos = new Vector(0, 150);
    Vector nopeusAlas = new Vector(0, -150);
    Vector nopeusOikealle = new Vector(150, 0);
    Vector nopeusVasemmalle = new Vector(-150, 0);

    PhysicsObject jalkapallo;
    PhysicsObject pelaaja1;
    PhysicsObject vihu1;
    PhysicsObject vihu2;

    public override void Begin()
    {
        ClearGameObjects();
        ClearControls();
        LuoKentta();
        AsetaOhjaimet();

    }
    void LuoKentta()
    {
        jalkapallo = PhysicsObject.CreateStaticObject(30.0, 30.0);
        jalkapallo.Shape = Shape.Circle;
        jalkapallo.X = 0.0;
        jalkapallo.Y = 0.0;
        jalkapallo.Restitution = 1.0;
        Add(jalkapallo);

        TileMap ruudut = TileMap.FromLevelAsset("kentta1.txt");
        ruudut.SetTileMethod('*', LuoSeina);
        ruudut.Execute(15.0, 15.0);

        pelaaja1 = LuoPelaaja(Level.Right - 50.0, 0.0);

        vihu1 = LuoVihu(Level.Right - 100.0, 20.0);
        vihu2 = LuoVihu(Level.Left + 100.0, 20.0);

        Level.CreateBorders(0.0, false);
        Level.Background.Color = Color.Green;
        Camera.ZoomToLevel();

    }

    PhysicsObject LuoPelaaja(double x, double y)
    {
        PhysicsObject pelaaja = new PhysicsObject(15.0, 15.0);
        pelaaja.Shape = Shape.Rectangle;
        pelaaja.X = x;
        pelaaja.Y = y;
        pelaaja.Restitution = 0.0;
        this.Add(pelaaja);
        return pelaaja;
    }

    private void LuoSeina(Vector paikka, double x, double y)
    {
        PhysicsObject seina = PhysicsObject.CreateStaticObject(x, y);
        seina.Position = paikka;
        seina.Shape = Shape.Rectangle;
        seina.Color = Color.Gray;
        this.Add(seina);
    }

    PhysicsObject LuoVihu(double x, double y)
    {
        vihu1 = new PhysicsObject(15.0, 15.0);
        vihu1.Shape = Shape.Diamond;
        vihu1.X = x;
        vihu1.Y = y;
        vihu1.Restitution = 0.0;
        AddCollisionHandler(pelaaja1, vihu1,);
        this.Add(vihu1);
        return vihu1;
    }
    
    private void AsetaOhjaimet()
    {
        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Liikuta Pelaaja ylös", pelaaja1, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, pelaaja1, Vector.Zero);

        Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Liikuta pelaaja vasen", pelaaja1, nopeusVasemmalle);
        Keyboard.Listen(Key.Left, ButtonState.Released, AsetaNopeus, null, pelaaja1, Vector.Zero);

        Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Liikuta pelaaja oikea", pelaaja1, nopeusOikealle);
        Keyboard.Listen(Key.Right, ButtonState.Released, AsetaNopeus, null, pelaaja1, Vector.Zero);

        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Liikuta pelaaja alas", pelaaja1, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, pelaaja1, Vector.Zero);


        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    private void AsetaNopeus(PhysicsObject pelaaja1, Vector nopeus)
    {
       pelaaja1.Velocity = nopeus;
    }

    /*private void PalloOsui()
    {
        PosautaVihollinen()
    }*/
}

