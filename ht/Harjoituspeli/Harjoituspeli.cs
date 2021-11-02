using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
/// <summary>
/// @author Anton Kelo
/// @version 
///
/// 
/// </summary>
public class Harjoituspeli : PhysicsGame
{
    Vector nopeusYlos = new Vector(0, 150);
    Vector nopeusAlas = new Vector(0, -150);
    Vector nopeusOikealle = new Vector(150, 0);
    Vector nopeusVasemmalle = new Vector(-150, 0);

    PhysicsObject jalkapallo;
    PhysicsObject pelaaja;
    PhysicsObject vihu1;
    PhysicsObject vihu2;

    /// <summary>
    /// 
    /// </summary>
    public override void Begin()
    {
        ClearGameObjects();
        ClearControls();
        LuoKentta();
        AsetaOhjaimet();
        AddCollisionHandler(pelaaja, vihu1, CollisionHandler.DestroyTarget);
        AddCollisionHandler(pelaaja, vihu2, CollisionHandler.DestroyTarget);

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
        /// 
        /// </summary>
        jalkapallo = PhysicsObject.CreateStaticObject(30.0, 30.0, Shape.Circle);
        jalkapallo.X = 0.0;
        jalkapallo.Y = 0.0;
        jalkapallo.Restitution = 0.0;
        jalkapallo.Image = LoadImage("eurooppa.png");
        Add(jalkapallo);
        /// <summary>
        /// 
        /// </summary>
        pelaaja = LuoPelaaja(Level.Right - 50.0, 0.0);
        /// <summary>
        /// 
        /// </summary>
        vihu1 = LuoVihu(Level.Right - 45.0, 150.0);
        //vihu1.Image = LoadImage("amerikkalainen.png");
        PathFollowerBrain polkuaivo = new PathFollowerBrain(30);
        vihu1.Brain = polkuaivo;
        polkuaivo.Active = true;
        List<Vector> polku = new List<Vector>();
        polku.Add(new Vector(65.0, 150.0));
        polku.Add(new Vector(65.0, 10));
        polku.Add(new Vector(0.0, 0.0));
        polkuaivo.Path = polku;

        vihu2 = LuoVihu(Level.Left + 50.0, 150.0);
        //vihu2.Image = LoadImage("amerikkalainen.png");
        PathFollowerBrain polkuaivo2 = new PathFollowerBrain(30);
        vihu2.Brain = polkuaivo2;
        polkuaivo2.Active = true;
        List<Vector> polku2 = new List<Vector>();
        polku2.Add(new Vector(-185.0, 50.0));
        polku2.Add(new Vector(-10.0, 20.0));
        polku2.Add(new Vector(0.0, 0.0));
        polkuaivo2.Path = polku2;



    }
    /// <summary>
    /// Luodaan pelissä liikuteltava pelaaja
    /// </summary>
    /// <param name="x">Leveys</param>
    /// <param name="y">Korkeus</param>
    /// <returns>pelaajan pääohjelmaan</returns>
    PhysicsObject LuoPelaaja(double x, double y)
    {
        PhysicsObject pelaaja = new PhysicsObject(15.0, 15.0);
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
    private void LuoSeina(Vector paikka, double x, double y)
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
    PhysicsObject LuoVihu(double x, double y)
    {
        PhysicsObject vihu = new PhysicsObject(15.0, 15.0);
        vihu.Shape = Shape.Diamond;
        vihu.X = x;
        vihu.Y = y;
        vihu.Restitution = 0.3;
        vihu.Image = LoadImage("amerikkalainen.png");

        this.Add(vihu);
        return vihu;
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



}


