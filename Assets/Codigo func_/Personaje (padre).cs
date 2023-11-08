using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;

public abstract class Personaje : MonoBehaviour
{
    public struct Entorno
    {
        public int[,] datos;
    }

    private SpriteRenderer sprite1;
    public Sprite sSalto;
    public Sprite sDfault;
    public Sprite sMuerto;
    private Vector3 spawnpos;
    protected abstract bool MoveUpInput();
    protected abstract bool MoveDownInput();
    protected abstract bool MoveLeftInput();
    protected abstract bool MoveRightInput();
    private void Awake()
    {
        sprite1 = GetComponent<SpriteRenderer>();
        spawnpos = transform.position;
    }
    private void Update()
    {
        // Mover hacia arriba
        if (MoveUpInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            MoveCharacter(Vector3.up);
        }
        // Mover hacia abajo
        else if (MoveDownInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            MoveCharacter(Vector3.down);
        }
        // Mover hacia la izquierda
        else if (MoveLeftInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            MoveCharacter(Vector3.left);
        }
        // Mover hacia la derecha
        else if (MoveRightInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            MoveCharacter(Vector3.right);
        }
    }
    public void MoveCharacter(Vector3 direc)
    {
        Vector3 dest = transform.position + direc;
        Collider2D barrera = Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("barrera"));
        Collider2D plataforma = Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("plataforma"));
        Collider2D obs = Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("obstaculo"));

        if (barrera != null) { return; }

        if (plataforma != null)
        {
            transform.SetParent(plataforma.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obs != null && plataforma == null)
        {
            transform.position = dest;
            Muerto();
        }
        else
        {
            StartCoroutine(Salto(dest));
        }
    }
    private IEnumerator Salto(Vector3 desti)
    {
        Vector3 stPosition = transform.position;
        float trans = 0f;
        float duracion = 0.1f;
        sprite1.sprite = sSalto;

        while (trans < duracion)
        {
            float t = trans / duracion;
            transform.position = Vector3.Lerp(stPosition, desti, t);
            trans += Time.deltaTime;
            yield return null;
        }

        transform.position = desti;
        sprite1.sprite = sDfault;
    }
    public void respawn()
    {
        StopAllCoroutines();
        transform.rotation= Quaternion.identity;
        transform.position = spawnpos;
        sprite1.sprite = sDfault;
        gameObject.SetActive(true);
        enabled = true;
    }
    protected void Muerto()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        sprite1.sprite = sMuerto;
        enabled = false;

        Invoke(nameof(respawn), 1f);//para que le de un time out cuando muera
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("obstaculo") && transform.parent == null)
        {
            Muerto(); /*sprite1.sprite = sMuerto;*/
        }
    }
    public Entorno ObtenerEntorno(int rango)
    {
        Vector3 posicionActual = transform.position;
        Entorno entorno = new Entorno();
        entorno.datos = new int[rango * 2 + 1, rango * 2 + 1];
        for (int i = -rango; i <= rango; i++)
        {
            for (int j = -rango; j <= rango; j++)
            {
                Vector3 posicionAnalizada = posicionActual + new Vector3(i, j, 0);
            }
        }

        return entorno;
    }
    public void connect()
    {
        Socket prueba = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint address = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        prueba.Connect(address);

        Console.WriteLine("Conectado con éxito");
        Console.WriteLine("Ingrese la información para enviar");

        string data = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et" +
             " dolore magna aliqua. Eget egestas purus viverra accumsan. Justo nec ultrices dui sapien" +
             " eget mi proin sed. Euismod in pellentesque massa placerat duis ultricies lacus. Amet mattis vulputate en" +
             "im nulla aliquet porttitor lacus luctus accumsan. Ornare lectus sit amet est placerat. Senectus et netus et" +
             " malesuada. Scelerisque viverra mauris in aliquam sem fringilla ut. Risus ultricies tristique nulla aliquet" +
             " enim tortor. Eget egestas purus viverra accumsan in nisl nisi scelerisque eu. Enim eu turpis egestas pretium" +
             ". Mi eget mauris pharetra et ultrices neque ornare aenean euismod. Ut sem nulla pharetra diam. Euismod in pel" +
             "lentesque massa placerat. Ipsum consequat nisl vel pretium lectus. Id aliquet lectus proin nibh.Nec feugiat nisl " +
             "pretium fusce id velit ut tortor pretium. Tempor orci eu lobortis elementum nibh. Quam id leo in vitae turpis massa " +
             "sed elementum tempus. Enim ut sem viverra aliquet eget sit amet. Sed felis eget velit aliquet sagittis id consectetur " +
             "purus ut. Sapien pellentesque habitant morbi tristique senectus et netus. Justo laoreet sit amet cursus sit amet. Nulla " +
             "malesuada pellentesque elit eget gravida cum. Tempus imperdiet nulla malesuada pellentesque elit eget. Vulputate sapien " +
             "nec sagittis aliquam malesuada. Fermentum dui faucibus in ornare quam viverra orci sagittis eu. Ipsum consequat nisl vel pretium. Volutpat maecenas volutpat blandit aliquam etiam erat velit. Arcu cursus euismod quis viverra nibh. In pellentesque massa placerat duis ultricies lacus sed turpis tincidunt. Tortor id aliquet lectus proin nibh nisl. Ut lectus arcu bibendum at varius vel pharetra vel. Sodales neque sodales ut etiam sit amet nisl purus in. Blandit volutpat maecenas volutpat blandit aliquam. Scelerisque eu ultrices vitae auctor eu augue ut.Convallis tellus id interdum velit laoreet id. Scelerisque felis imperdiet proin " +
             "fermentum leo vel orci porta. Urna condimentum mattis pellentesque id. Morbi quis commodo odio aenean. Feugiat nisl pretium fusce id velit. Sem et tortor consequat id. Nisi quis eleifend quam adipiscing vitae proin sagittis nisl rhoncus. Volutpat diam ut venenatis tellus in metus vulputate. Magna fermentum iaculis eu non diam phasellus vestibulum lorem. Convallis a cras semper auctor neque vitae tempus quam. Viverra adipiscing at in tellus integer feugiat scelerisque varius morbi. Elementum curabitur vitae nunc sed.Sapien eget mi proin sed libero enim sed. Ac tortor vitae purus faucibus. Elementum integer enim neque volutpat. Viverra mauris" +
             " in aliquam sem. Amet nisl suscipit adipiscing bibendum est. Ut tristique et egestas quis ipsum suspendisse ultrices gravida. Vulputate enim nulla aliquet porttitor lacus. Habitant morbi tristique senectus et netus et malesuada fames. Imperdiet dui accumsan sit amet nulla facilisi morbi tempus. At quis risus sed vulputate odio. Nascetur ridiculus mus mauris vitae ultricies leo integer malesuada nunc. Faucibus a pellentesque sit amet porttitor eget dolor. Molestie a iaculis at erat pellentesque adipiscing commodo elit at. Mollis aliquam ut porttitor leo a diam sollicitudin tempor id. Id leo in vitae turpis massa sed elementum tempus. Mattis molestie a iaculis at erat pellentesque. Convallis convallis tellus id interdum velit laoreet id.Consectetur adipiscing elit ut aliquam purus sit amet luctus venenatis. Sed faucibus turpis in eu mi bibendum. Vitae tempus quam pellentesque nec nam aliquam sem et. Sit amet porttitor eget dolor morbi non. Sapien pellentesque habitant morbi tristique senectus et netus et. Consectetur a erat nam at lectus. Lorem dolor sed viverra ipsum nunc aliquet bibendum enim facilisis. Sit amet volutpat consequat mauris nunc congue nisi. Lacinia at quis risus sed vulputate odio ut. Tincidunt lobortis feugiat vivamus at augue eget arcu dictum. Risus in hendrerit gravida rutrum quisque non tellus. Euismod elementum nisi quis eleifend quam adipiscing vitae proin. Elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus at. Tortor at risus viverra adipiscing at in tellus integer feugiat. Velit scelerisque in dictum non consectetur a erat nam at. Blandit massa enim nec dui nunc mattis enim ut tellus.Pretium nibh ipsum consequat nisl vel pretium. Ante metus dictum at tempor commodo ullamcorper a lacus vestibulum. Eu ultrices vitae auctor eu augue ut lectus arcu. Massa tincidunt dui ut ornare lectus. Pharetra vel turpis nunc eget lorem dolor sed viverra ipsum. Orci a scelerisque purus semper. Sed cras ornare arcu dui vivamus arcu felis bibendum. Arcu odio ut sem nulla. Posuere urna nec tincidunt praesent semper feugiat nibh sed. Venenatis lectus magna fringilla urna. Ullamcorper velit sed ullamcorper morbi tincidunt. Facilisi morbi tempus iaculis urna id volutpat lacus laoreet non. Aliquam ultrices sagittis orci a scelerisque purus semper. Sagittis vitae et leo duis ut diam quam nulla. Id consectetur purus ut faucibus pulvinar elementum integer enim. Nam libero justo laoreet sit amet cursus sit amet. Elementum nibh tellus molestie nunc non blandit massa. Lorem dolor sed viverra ipsum nunc aliquet. Tincidunt nunc pulvinar sapien et ligula ullamcorper.Velit egestas dui id ornare arcu odio ut sem. Tristique senectus et netus et malesuada fames. Molestie at elementum eu facilisis sed odio morbi quis commodo. Et sollicitudin ac orci phasellus egestas tellus. Arcu non odio euismod lacinia at quis risus sed vulputate. Posuere urna nec tincidunt praesent semper feugiat nibh sed. Urna condimentum mattis pellentesque id. Aenean sed adipiscing diam donec adipiscing tristique risus. Pulvinar elementum integer enim neque volutpat ac tincidunt vitae. Eleifend donec pretium vulputate sapien nec. Orci porta non pulvinar neque laoreet suspendisse interdum. Quis viverra nibh cras pulvinar mattis nunc sed. Amet massa vitae tortor condimentum lacinia quis vel eros donec. Lacus vestibulum sed arcu non odio euismod lacinia. Amet commodo nulla facilisi nullam. Curabitur vitae nunc sed velit dignissim. Faucibus interdum posuere lorem ipsum. Porttitor massa id neque aliquam vestibulum morbi blandit.Blandit massa enim nec dui nunc mattis. Tristique senectus et netus et malesuada fames. Tempus iaculis urna id volutpat lacus laoreet non. Pellentesque massa placerat duis ultricies lacus. Nam aliquam sem et tortor consequat id porta. Pretium lectus quam id leo in vitae turpis massa. Vitae suscipit tellus mauris a diam maecenas sed enim ut. Luctus accumsan tortor posuere ac ut. In hendrerit gravida rutrum quisque. Pretium aenean pharetra magna ac placerat vestibulum lectus mauris ultrices. Mattis ullamcorper velit sed ullamcorper morbi tincidunt ornare. Laoreet suspendisse interdum consectetur libero id faucibus nisl tincidunt eget. Feugiat pretium nibh ipsum consequat nisl vel pretium lectus. Egestas quis ipsum suspendisse ultrices gravida dictum.Felis bibendum ut tristique et egestas quis. Aliquam etiam erat velit scelerisque in dictum non consectetur. Ornare aenean euismod elementum nisi quis eleifend. Nisl nunc mi ipsum faucibus vitae aliquet nec ullamcorper sit. Eget nunc lobortis mattis aliquam faucibus purus in massa tempor. Vulputate mi sit amet mauris commodo quis imperdiet massa tincidunt. Accumsan tortor posuere ac ut consequat semper viverra. Nec tincidunt praesent semper feugiat nibh. Mattis molestie a iaculis at erat pellentesque adipiscing commodo elit. Nisi quis eleifend quam adipiscing vitae proin sagittis nisl. Sit amet luctus venenatis lectus magna fringilla urna porttitor rhoncus. Sit amet cursus sit amet dictum sit amet justo. Sed enim ut sem viverra aliquet. Aliquet eget sit amet tellus cras adipiscing. Nullam non nisi est sit amet facilisis magna etiam tempor. Sed risus ultricies tristique nulla aliquet enim. Ornare arcu odio ut sem nulla pharetra. Urna nec tincidunt praesent semper feugiat. Pellentesque id nibh tortor id aliquet lectus proin. Vestibulum lorem sed risus ultricies.Quam vulputate dignissim suspendisse in est ante. Conge mauris rhoncus aenean vel elit scelerisque mauris. A erat nam at lectus urna duis convallis. Pulvinar etiam non quam lacus suspendisse. Cras fermentum odio eu feugiat. Volutpat lacus laoreet non curabitur gravida. In iaculis nunc sed augue lacus viverra vitae. Facilisis mauris sit amet massa. Faucibus in ornare quam viverra. Sed cras ornare arcu dui vivamus arcu felis. Viverra justo nec ultrices dui sapien. Viverra aliquet eget sit amet tellus cras adipiscing enim eu. Ac orci phasellus egestas tellus. Faucibus in ornare quam viverra orci.Accumsan tortor posuere ac ut consequat. Urna molestie at elementum eu facilisis sed odio morbi. Cras ornare arcu dui vivamus arcu felis. Ultrices mi tempus imperdiet nulla malesuada pellentesque elit eget gravida. Lorem donec massa sapien faucibus et molestie ac. Placerat vestibulum lectus mauris ultrices eros in cursus turpis massa. Senectus et netus et malesuada fames ac turpis. Malesuada pellentesque elit eget gravida cum sociis natoque. Egestas integer eget aliquet nibh praesent tristique magna. Elementum pulvinar etiam non quam lacus suspendisse faucibus interdum. Massa sed elementum tempus egestas sed. Aliquam id diam maecenas ultricies mi eget mauris pharetra et. Placerat in egestas erat imperdiet sed euismod nisi porta lorem. Dui ut ornare lectus sit amet. Mauris ultrices eros in cursus. Enim nunc faucibus a pellentesque sit amet porttitor eget.Aenean euismod elementum nisi quis eleifend quam. Nibh mauris cursus mattis molestie a iaculis. Libero nunc consequat interdum varius sit. Elementum nisi quis eleifend quam adipiscing vitae. Semper eget duis at tellus at urna condimentum mattis pellentesque. Interdum velit laoreet id donec ultrices tincidunt. Donec ultrices tincidunt arcu non sodales neque sodales ut etiam. Ut porttitor leo a diam sollicitudin. Eu mi bibendum neque egestas congue quisque egestas diam in. Ultricies integer quis auctor elit sed vulputate mi sit amet. Ullamcorper sit amet risus nullam eget felis eget nunc lobortis. Non curabitur gravida arcu ac tortor.Diam maecenas ultricies mi eget mauris. Fames ac turpis egestas maecenas pharetra convallis posuere morbi leo. Molestie at elementum eu facilisis sed odio morbi quis commodo. Elit ullamcorper dignissim cras tincidunt lobortis. Orci eu lobortis elementum nibh. Hendrerit dolor magna eget est lorem ipsum dolor sit. Sagittis purus sit amet volutpat consequat mauris. Est velit egestas dui id ornare. Hac habitasse platea dictumst quisque sagittis purus sit. Congue mauris rhoncus aenean vel. Nunc vel risus commodo viverra maecenas accumsan. Nulla facilisi etiam dignissim diam quis enim lobortis scelerisque. Neque sodales ut etiam sit. Enim praesent elementum facilisis leo vel.Ultrices sagittis orci a scelerisque purus semper. Ullamcorper dignissim cras tincidunt lobortis feugiat vivamus at augue eget. Sed libero enim sed faucibus turpis in. Morbi quis commodo odio aenean. Nunc sed id semper risus in hendrerit gravida. Justo nec ultrices dui sapien eget mi ";

        byte[] datasend = Encoding.Default.GetBytes(data);
        string length = datasend.Length.ToString();
        byte[] header = Encoding.Default.GetBytes(length.ToString() + "|txt");
        prueba.Send(header, 0, header.Length, 0);
        int bytesSent = 0;
        int chunkSize = 254; // Tamaño del fragmento

        // Enviar los datos en fragmentos
        while (bytesSent < datasend.Length)
        {
            int remainingBytes = datasend.Length - bytesSent;
            int currentChunkSize = Math.Min(chunkSize, remainingBytes);
            prueba.Send(datasend, bytesSent, currentChunkSize, 0);
            bytesSent += currentChunkSize;
        }

        //recibimos
        byte[] arrBy = new byte[256];
        int a = prueba.Receive(arrBy, 0, arrBy.Length, 0);//recibimos los caracteres en forma de bytes
        Array.Resize(ref arrBy, a);
        Console.WriteLine("Servidor dice: " + Encoding.Default.GetString(arrBy));

        prueba.Close();
        Console.WriteLine("Presione una tecla para terminar");
        Console.ReadKey();
    }
}