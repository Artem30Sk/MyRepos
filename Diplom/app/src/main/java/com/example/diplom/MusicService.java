package com.example.diplom;

import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.Build;
import android.os.IBinder;
import java.util.ArrayList;
import android.content.ContentUris;
import android.media.AudioManager;
import android.net.Uri;
import android.os.Binder;
import android.os.PowerManager;
import android.support.v4.media.session.MediaSessionCompat;
import android.util.Log;
import androidx.annotation.Nullable;
import androidx.core.app.NotificationCompat;
import androidx.core.app.NotificationManagerCompat;
import static  com.example.diplom.WebviewActivity.i;
import static  com.example.diplom.WebviewActivity.songView;
import java.util.Random;
import android.app.Notification;
import android.app.PendingIntent;
import android.view.View;

public class MusicService  extends Service implements
        MediaPlayer.OnPreparedListener, MediaPlayer.OnErrorListener,
        MediaPlayer.OnCompletionListener {
    private final IBinder musicBind = new MusicBinder();
    //media player
    private MediaPlayer player;
    //song list
    public static ArrayList<Song> songs;
    //current position
    public static int songPosn;
    private String songTitle="";
    private static final int NOTIFY_ID=1;
    private boolean shuffle=false;
    private Random rand;
    private static final String CHANNEL_ID = "CHANNEL_ID";
    public static NotificationCompat.Builder notification;
    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return musicBind;
    }
    @Override
    public boolean onUnbind(Intent intent){
        player.stop();
        player.release();
        return false;
    }
    @Override
    public void onCompletion(MediaPlayer mp) {
        if(player.getCurrentPosition()>0){
            mp.reset();
            playNext();
        }
    }

    @Override
    public boolean onError(MediaPlayer mp, int what, int extra) {
        mp.reset();
        return false;
    }

    @Override
    public void onPrepared(MediaPlayer mp) {
        player.setOnPreparedListener(this);
        mp.start();
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            Intent notIntent = new Intent(this, MainActivity.class);
            notIntent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_NEW_TASK);
            PendingIntent pendInt = PendingIntent.getActivity(this, 0,
                    notIntent, PendingIntent.FLAG_UPDATE_CURRENT);
            NotificationChannel channel = new NotificationChannel(CHANNEL_ID,"Player",NotificationManager.IMPORTANCE_LOW);
            Notification.Builder builder = new Notification.Builder(this,CHANNEL_ID);
            notification = new NotificationCompat.Builder(this,CHANNEL_ID)
                    .setSmallIcon(R.drawable.android_music_player_play)
                    .setTicker(songTitle)
                    .setOngoing(true)
                    .setContentTitle("Playing")
                    .setContentText(songTitle);
            Notification not = builder.build();
            startForeground(NOTIFY_ID, not);
        }
    }
    public void onCreate(){
        //create the service
        super.onCreate();
//initialize position
        songPosn=0;
//create player
        player = new MediaPlayer();
        initMusicPlayer();
        rand=new Random();
    }
    public void initMusicPlayer(){
        //set player properties
        player.setWakeMode(getApplicationContext(),
                PowerManager.PARTIAL_WAKE_LOCK);
        player.setAudioStreamType(AudioManager.STREAM_MUSIC);
    }
    public void setList(ArrayList<Song> theSongs){
        songs=theSongs;
    }
    public class MusicBinder extends Binder {
        MusicService getService() {
            return MusicService.this;
        }
    }
    public void playSong(){
        //play a song

        player.reset();
        //get song
        Song playSong = songs.get(songPosn);
        //get id
        songTitle=playSong.getTitle();
        long currSong = playSong.getID();
        //set uri
        Uri trackUri = ContentUris.withAppendedId(
                android.provider.MediaStore.Audio.Media.EXTERNAL_CONTENT_URI,
                currSong);
        try{
            player.setDataSource(getApplicationContext(), trackUri);
        }
        catch(Exception e){
            Log.e("MUSIC SERVICE", "Error setting data source", e);
        }
        player.prepareAsync();

    }
    public void setSong(int songIndex){
        songPosn=songIndex;
    }
    public int getPosn(){
        return player.getCurrentPosition();
    }

    public int getDur(){
        return player.getDuration();
    }

    public boolean isPng(){
        return player.isPlaying();
    }

    public void pausePlayer(){
        player.pause();
    }

    public void seek(int posn){
        player.seekTo(posn);
    }

    public void go(){
        player.start();
    }
    public void playPrev(){
        songView.getChildAt(songPosn).setBackgroundColor(getResources().getColor(R.color.white));
        songView.getChildAt(i).setBackgroundColor(getResources().getColor(R.color.white));
        songPosn--;
        if(songPosn < 0) {
            songPosn++;
        }
            songView.getChildAt(songPosn).setBackgroundColor(getResources().getColor(R.color.lightgrey));
        playSong();
       }
    public void playNext() {

        if (shuffle) {
            int newSong = songPosn;
            while (newSong == songPosn) {
                newSong = rand.nextInt(songs.size());
            }
            if(songPosn>=0 || songPosn<=100){
                songView.getChildAt(songPosn).setBackgroundColor(getResources().getColor(R.color.white));
                songView.getChildAt(i).setBackgroundColor(getResources().getColor(R.color.white));

            }
            songPosn = newSong;
            songView.getChildAt(songPosn).setBackgroundColor(getResources().getColor(R.color.lightgrey));
        } else {
            songView.getChildAt(songPosn).setBackgroundColor(getResources().getColor(R.color.white));
            songView.getChildAt(i).setBackgroundColor(getResources().getColor(R.color.white));
            songPosn++;
            if(songPosn>=songs.size())songPosn=0;
            songView.getChildAt(songPosn).setBackgroundColor(getResources().getColor(R.color.lightgrey));
        }
        playSong();
    }

    @Override
    public void onDestroy() {
        stopForeground(true);
    }

    public void offShuffle(){
            shuffle=false;
    }
    public void setShuffle(){
        shuffle=true;
    }
}
