/**
		まさおコンストラクション ＦＸ

		簡易設定アプレット
*/

//  インポート
import java.applet.*;
import java.awt.*;
import java.awt.event.*;


/**  簡易設定アプレット			*/
public class MasaoKani extends MasaoFXApplet {
	MasaoKani Applet1;			//  このクラス    JavaScript からの移植用

	//  ユーザー  グローバル変数
	int boss_v;					//  ボスの有無
	int boss_x;					//  ボスのＸ座標
	int boss_y;					//  ボスのＹ座標
	int boss_width;				//  ボスのＸ座標
	int boss_height;			//  ボスのＹ座標
	int boss_vx;				//  Ｘ方向の速度
	int boss_vy;				//  Ｙ方向の速度
	int boss_kakudo;			//  ボスの角度
	int boss_x_shoki;			//  ボスの初期Ｘ座標
	int boss_y_shoki;			//  ボスの初期Ｙ座標
	int boss_speed;				//  ボスの最高速度
	int boss_hp;				//  ボスのＨＰ
	int boss_hp_max;			//  ボスの最大ＨＰ
	int boss_jyoutai;			//  ボスの状態
	int boss_jyoutai_b;			//  ボスの状態バックアップ用
	int boss_bc;				//  ボスの爆発カウンター
	int boss_ac;				//  ボスのアニメーションカウンター
	int boss_shageki_c;			//  ボスの射撃カウンター
	int boss_shageki_c2;		//  ボスの射撃カウンター２
	String boss_name;			//  ボスの名前
	int boss_ugoki;				//  ボスの動き方
	int boss_waza;				//  ボスの技
	int boss_waza_wait;			//  ボスの技使用後の待機時間
	int boss_fumeru_f;			//  踏めるかフラグ
	int boss_tail_f;			//  しっぽ攻撃有効フラグ
	int boss_destroy;			//  ボスを倒した場合
	Image boss_left1_img;		//  ボスの左向き画像
	Image boss_right1_img;		//  ボスの右向き画像
	Image boss_left2_img;		//  ボスの左向き画像
	Image boss_right2_img;		//  ボスの右向き画像
	Image boss_tubure_left_img;	//  ボスの潰れた左向き画像
	Image boss_tubure_right_img;//  ボスの潰れた右向き画像
	int sl_x;					//  スクロールロックするＸ座標
	int bgm_switch;				//  ＢＧＭ演奏		1  しない
								//					2  する
								//					3  ループ演奏
	int bgm_gen = 0;			//  現在演奏中の曲
	AudioClip[] bgm = new AudioClip[10];
								//  ＢＧＭ  0	なし
								//			1	ステージ１
								//			2	ステージ２
								//			3	ステージ３
								//			4	ステージ４
								//			5	ボス
								//			6	タイトル画面
								//			7	エンディング
								//			8	地図画面
	String[] bgm_filename = new String[10];

	int[] ximage_view_x = new int[4];		//  Ｘ座標指定画像		表示開始Ｘ座標
	int[] ximage_x = new int[4];			//  Ｘ座標指定画像		表示Ｘ座標
	int[] ximage_y = new int[4];			//  Ｘ座標指定画像		表示Ｙ座標
	int[] ximage_c = new int[4];			//  Ｘ座標指定画像		カウンター
	Image[] ximage_img = new Image[4];		//  Ｘ座標指定画像		画像


	//**  アプレット    ap
	//  初期化    ap
	public void init() {


		//  まさおコンストラクション配置
		mc = new MasaoGhost(this,this);


		//  ユーザー初期化エリア  ここから

		//  ユーザー初期化用
		userInitJS();

		//  ユーザー初期化エリア  ここまで


	}


	//  描画    ap
	public void paint(Graphics g){
		String nst;


		//  ゲーム画面を描画
		if(my_offscreen_img != null) {
			g.drawImage(my_offscreen_img,0,0,this);
		}
		else {
			//  起動中のメッセージ

			//  塗りつぶす
			g.setColor(Color.black);
			g.fillRect(0,0,512,320);

			nst = getParameter("now_loading");

			if(nst != null) {
				g.setColor(Color.white);
				g.setFont(new Font("Dialog",Font.PLAIN,16));
				g.drawString(getParameter("now_loading"),32,160);
			}
		}
	}


	//  停止    ap
	public void stop() {


		//  まさおコンストラクション停止
		mc.stop();

		//  ＢＧＭ停止
		if(bgm_switch == 2  ||  bgm_switch == 3) {
			//  ＢＧＭ使用中

			//  現在再生中の曲を停止
			if(bgm[bgm_gen] != null) {
				bgm[bgm_gen].stop();
			}
		}
	}


	//  まさおイベント
	//  描画直前にメインプログラムから呼び出される
	//
	//  Graphics offscreen_g;			//  オフスクリーン
	//  Image offscreen_img;			//  オフスクリーン
	public void masaoEvent(Graphics offscreen_g,Image offscreen_img) {


		//  ユーザープログラムエリア  ここから

		int mode;


		//  モードを取得
		mode = Applet1.getMode();

		if(mode == 1) {
			//  タイトル画面

			//  JavaScript タイトル画面表示中を呼び出す
			userTitleJS(offscreen_g);
		}
		else if(mode >= 100  &&  mode < 200) {
			//  ゲーム中
			if(Applet1.getJSMes() == 1) {
				//  ゲーム開始

				//  JavaApplet からのメッセージをクリアー
				Applet1.setJSMes("2");

				//  ゲーム開始を呼び出す
				userGameStartJS();
			}
			else {
				//  ゲーム中を呼び出す
				userGameJS(offscreen_g,Applet1.getViewXReal(),Applet1.getViewYReal());
			}
		}
		else if(mode == 200) {
			//  ゲームオーバー
			userGameoverJS(offscreen_g);
		}
		else if(mode == 300) {
			//  エンディング
			userEndingJS(offscreen_g);
		}
		else if(mode == 400) {
			//  地図画面
			userChizuJS(offscreen_g);
		}

		//  ユーザープログラムエリア  ここまで


		//  ゲーム画面のイメージを取得
		my_offscreen_img = offscreen_img;

		//  再描画
		repaint();
	}




	//  ユーザーメソッド
	//
	//  メソッド名は MasaoXJSS2.class 用と同じ

	//  JavaApplet 起動時に Java から１回だけ呼び出される
	void userInitJS() {


		//  このクラスを取得
		Applet1 = this;

		//  ＢＧＭの初期化
		initBGM();
	}


	//  タイトル画面表示中に Java から呼び出される
	void userTitleJS(Graphics Offscreen_g) {


		//  ＢＧＭ  タイトル画面
		playBGM(6);
	}


	//  ゲーム開始時に Java から１回だけ呼び出される
	void userGameStartJS() {
		int n;
		String nst;


		//  主人公のＨＰ設定
		n = getParamInt("j_hp");
		if(n < 1) n = 1;
		if(n > 1) {
			Applet1.setMyMaxHP(Integer.toString(n));

			//  主人公のＨＰ表示
			nst = getParameter("j_hp_name");
			if(nst == null) nst = "";
			Applet1.showMyHP(nst);
		}

		//  主人公のファイヤーボール標準装備
		n = getParamInt("j_equip_fire");
		if(n == 2) {
			Applet1.equipFire();
		}

		//  主人公のグレネード装備数
		n = getParamInt("j_equip_grenade");
		if(n >= 1) {
			Applet1.equipGrenade(Integer.toString(n));
		}

		//  Ｘ座標指定画像  初期化
		initXImage();

		//  ボスを初期化する
		initBoss();

		//  ＢＧＭ再生
		n = Applet1.getMode();
		if(n == 102) {
			//  ステージ２
			playBGM(2);
		}
		else if(n == 103) {
			//  ステージ３
			playBGM(3);
		}
		else if(n == 104) {
			//  ステージ４
			playBGM(4);
		}
		else {
			//  ステージ１
			playBGM(1);
		}
	}


	//  ゲーム中    描画直前に Java から呼び出される
	void userGameJS(Graphics Offscreen_g,int view_x,int view_y) {


		//  ボスを動かす
		if(boss_jyoutai > 0) {
			moveBoss(Offscreen_g,view_x,view_y);
		}

		//  Ｘ座標指定画像を動かす
		moveXImage(Offscreen_g,view_x);

		if(Applet1.getMode() == 150) {
			//  ＢＧＭ再生  ボス
			playBGM(5);
		}
	}


	//  ゲームオーバー    描画直前に Java から呼び出される
	void userGameoverJS(Graphics Offscreen_g) {


			//  ＢＧＭ再生  ゲームオーバー
			playBGM(0);
	}


	//  エンディング    描画直前に Java から呼び出される
	void userEndingJS(Graphics Offscreen_g) {


			//  ＢＧＭ再生  エンディング
			playBGM(7);
	}


	//  地図画面    描画直前に Java から呼び出される
	void userChizuJS(Graphics Offscreen_g) {


			//  ＢＧＭ再生  地図画面
			playBGM(8);
	}


	//  param タグから value を int 型で取得する
	//  String st1;		//  name 属性
	//  戻り値 int		//  エラーの場合は -1
	int getParamInt(String st1) {
		int modori = -1;


		try {
			modori = Integer.parseInt(getParameter(st1));
		}
		catch (NumberFormatException e) {
			modori = -1;
		}

		return(modori);
	}


	//  ＢＧＭを初期化する
	void initBGM() {
		int i,n;


		//  初期化
		for(i = 0;i <= 9;i++) {
			bgm_filename[i] = null;
			bgm[i] = null;
		}

		//  ＢＧＭのループ再生
		bgm_switch = getParamInt("bgm_switch");
		if(bgm_switch < 1  ||  bgm_switch > 3) bgm_switch = 1;

		if(bgm_switch == 2  ||  bgm_switch == 3) {
			//  ファイル名を取得
			bgm_filename[1] = getParameter("filename_bgm_stage1");
			bgm_filename[2] = getParameter("filename_bgm_stage2");
			bgm_filename[3] = getParameter("filename_bgm_stage3");
			bgm_filename[4] = getParameter("filename_bgm_stage4");
			bgm_filename[5] = getParameter("filename_bgm_boss");
			bgm_filename[6] = getParameter("filename_bgm_title");
			bgm_filename[7] = getParameter("filename_bgm_ending");
			bgm_filename[8] = getParameter("filename_bgm_chizu");

			//  AudioClip を取得
			for(i = 1;i <= 8;i++) {
				if(bgm_filename[i] != null) {
					if(bgm_filename[i].length() > 3) {
						bgm[i] = getAudioClip(getDocumentBase(),bgm_filename[i]);
					}
				}
			}
		}
	}


	//  ＢＧＭを再生する
	//  int k;				//  曲コード
	void playBGM(int k) {


		//  再生しない
		if(bgm_switch == 1) return;

		if(bgm_gen != k) {
			//  現在再生中の曲と違う
			if(bgm_filename[bgm_gen] != null  &&  bgm_filename[k] != null) {
				if(bgm_filename[bgm_gen].equals(bgm_filename[k])  == true) {
					//  再生予定の曲とファイル名が同じ

					//  現在の曲を設定
					bgm_gen = k;

					//  処理終了
					return;
				}
			}

			//  現在再生中の曲を停止
			if(bgm[bgm_gen] != null) {
				bgm[bgm_gen].stop();
			}

			//  現在の曲を設定
			bgm_gen = k;

			//  曲を再生
			if(bgm[bgm_gen] != null) {
				if(bgm_switch == 3) {
					//  ループ再生
					bgm[bgm_gen].loop();
				}
				else {
					//  再生
					bgm[bgm_gen].play();
				}
			}
		}
	}


	//  ボスを初期化する
	void initBoss() {
		int n;
		String nst;


		//  param タグでボスの能力を設定

		//  状態
		boss_v = getParamInt("oriboss_v");
		if(boss_v >= 2) {
			//  ボスを配置する

			boss_jyoutai = 30;					//  状態  待機中
			boss_jyoutai_b = boss_jyoutai;		//  状態  バックアップ
			boss_ac = 0;						//  アニメーションカウンター

			//  名前
			boss_name = getParameter("oriboss_name");
			if(boss_name == null) boss_name = "";

			//  HP
			boss_hp = getParamInt("oriboss_hp");
			if(boss_hp < 1) boss_hp = 1;
			boss_hp_max = boss_hp;

			if(boss_v == 3) {
				//  センクウザの文字 Z の位置に設置
				if(mc.mp.co_b.c == 300) {
					//  センクウザ配置中
					mc.mp.co_b.c = 0;		//  センクウザ撤去

					//  座標を設定
					boss_x = mc.mp.co_b.x;
					boss_y = mc.mp.co_b.y+16;
				}
				else {
					//  ボスを配置しない
					boss_jyoutai = 0;
				}
			}
			else {
				//  座標指定で設置

				//  Ｘ座標
				boss_x = getParamInt("oriboss_x");
				if(boss_x < 0) boss_x = 0;
				if(boss_x > 179) boss_x = 179;
				boss_x = (boss_x+1)*32;

				//  Ｙ座標
				boss_y = getParamInt("oriboss_y");
				if(boss_y < 0) boss_y = 0;
				if(boss_y > 29) boss_y = 29;
				boss_y = (boss_y+10)*32;
			}

			//  初期座標
			boss_x_shoki = boss_x;
			boss_y_shoki = boss_y;

			//  幅
			boss_width = getParamInt("oriboss_width");
			if(boss_width < 32) boss_width = 32;

			//  幅
			boss_height = getParamInt("oriboss_height");
			if(boss_height < 32) boss_height = 32;

			//  速度
			boss_speed = getParamInt("oriboss_speed");
			if(boss_speed < 1) boss_speed = 1;
			//  boss_vx = boss_speed*(-1);
			boss_vx = -4;

			//  動き方
			boss_ugoki = getParamInt("oriboss_ugoki");
			if(boss_ugoki < 1) boss_ugoki = 1;
			if(boss_ugoki > 7) boss_ugoki = 7;

			//  技
			boss_waza = getParamInt("oriboss_waza");
			if(boss_waza < 0) boss_waza = 1;
			if(boss_waza > 18) boss_waza = 18;

			//  技使用後の待機時間
			boss_waza_wait = getParamInt("oriboss_waza_wait");
			if(boss_waza_wait < 1) boss_waza_wait = 1;

			//  踏めるかのフラグ
			boss_fumeru_f = getParamInt("oriboss_fumeru_f");
			if(boss_fumeru_f < 1  ||  boss_fumeru_f > 4) boss_fumeru_f = 1;

			//  しっぽ攻撃有効フラグ
			boss_tail_f = getParamInt("oriboss_tail_f");
			if(boss_tail_f != 2) boss_tail_f = 1;

			//  ボスを倒した場合
			boss_destroy = getParamInt("oriboss_destroy");
			if(boss_destroy != 2) boss_destroy = 1;

			if(boss_jyoutai > 0) {
				//  ボスが配置されている

				//  ボスの画像を取得
				MediaTracker mt = new MediaTracker(this);

				nst = Applet1.getParameter("filename_oriboss_left1");
				boss_left1_img = Applet1.getImage(Applet1.getDocumentBase(),nst);
				nst = Applet1.getParameter("filename_oriboss_left2");
				boss_left2_img = Applet1.getImage(Applet1.getDocumentBase(),nst);
				nst = Applet1.getParameter("filename_oriboss_right1");
				boss_right1_img = Applet1.getImage(Applet1.getDocumentBase(),nst);
				nst = Applet1.getParameter("filename_oriboss_right2");
				boss_right2_img = Applet1.getImage(Applet1.getDocumentBase(),nst);
				nst = Applet1.getParameter("filename_oriboss_tubure_left");
				boss_tubure_left_img = Applet1.getImage(Applet1.getDocumentBase(),nst);
				nst = Applet1.getParameter("filename_oriboss_tubure_right");
				boss_tubure_right_img = Applet1.getImage(Applet1.getDocumentBase(),nst);

				mt.addImage(boss_left1_img,0);
				mt.addImage(boss_left2_img,0);
				mt.addImage(boss_tubure_left_img,0);
				mt.addImage(boss_tubure_right_img,0);
				if(boss_ugoki == 2  ||  boss_ugoki >= 4  ||  boss_ugoki <= 7) {
					mt.addImage(boss_right1_img,0);
					mt.addImage(boss_right2_img,0);
				}

				try {
					mt.waitForID(0);			//  読み込むまで待つ
				}
				catch (InterruptedException e) {
				}

				//  スクロールロックを設定
				sl_x = boss_x-512;
				Applet1.setScrollLock(Integer.toString(sl_x));

				//  射撃カウンター
				//  boss_shageki_c = boss_waza_wait;
				boss_shageki_c = 5;
			}
		}
		else {
			//  ボスを配置しない
			boss_jyoutai = 0;
		}
	}


	//  ボスを動かす
	void moveBoss(Graphics Offscreen_g,int view_x,int view_y) {
		int n;


		//  ボスを動かす
		if(boss_jyoutai == 30) {
			//  待機中
			if(view_x >= sl_x) {
				//  スクロールロックされた

				//  出現中
				boss_jyoutai = 100;
			}

			//  処理終了
			return;
		}
		else if(boss_jyoutai == 20) {
			//  消えた状態でステージクリアー待機中
			//  カウントダウン
			boss_bc = boss_bc-1;
			if(boss_bc <= 0) {
				//  ステージクリアー
				Applet1.setStageClear();

				//  ボスの状態
				boss_jyoutai = 0;	//  死亡中
			}
		}
		else if(boss_jyoutai == 50) {
			//  爆発中

			//  カウントダウン
			boss_bc = boss_bc-1;
			if(boss_bc <= 0) {
				//  爆発終了

				//  ボスの状態
				boss_jyoutai = 0;	//  死亡中

				//  ボスのＨＰゲージを消す
				Applet1.hideGauge();

				//  １０００点加算
				Applet1.addScore(Integer.toString(1000));

				if(boss_destroy == 2) {
					//  ボスを倒すとステージクリアー
					boss_jyoutai = 20;		//  消えた状態でステージクリアー待機中
					boss_bc = 30;
				}
				else {
					//  人面星出現
					Applet1.setMapchip(Integer.toString(((int)(view_x/32)-1+6)),Integer.toString((int)(view_y/32)-10+4),Integer.toString(8));
				}
			}
		}
		else if(boss_jyoutai == 80) {
			//  潰れ中

			//  カウントダウン
			boss_bc = boss_bc-1;
			if(boss_bc <= 0) {
				//  潰れ状態終了

				//  ボスの状態
				boss_jyoutai = boss_jyoutai_b;
			}
		}
		else if(boss_jyoutai >= 100) {
			//  出現中

			if(boss_jyoutai == 100) {
				//  左へ移動中

				//  移動する
				boss_x = boss_x+boss_vx;

				if(boss_ugoki == 4  ||  boss_ugoki == 5) {
					//  左回り、右回りの場合
					if(boss_x <= sl_x+320) {
						//  射撃位置へ来た
						boss_x = sl_x+320;
						boss_vx = 0;

						if(boss_ugoki == 4) {
							//  動き方  左回り
							boss_jyoutai = 500;
							boss_kakudo = 0;				//  ボスの角度
						}
						else {
							//  動き方  右回り
							boss_jyoutai = 600;
							boss_kakudo = 0;				//  ボスの角度
						}

						boss_jyoutai_b = boss_jyoutai;		//  状態  バックアップ
					}
				}
				else if(boss_x <= sl_x+384) {
					//  射撃位置へ来た
					boss_x = sl_x+384;
					boss_vx = 0;

					if(boss_ugoki == 2) {
						//  動き方  左右移動
						boss_jyoutai = 300;
						boss_vx = boss_speed*(-1);			//  ボスの速度
					}
					else if(boss_ugoki == 3) {
						//  動き方  上下移動
						boss_jyoutai = 400;
						boss_vy = boss_speed*(-1);			//  ボスの速度
					}
					else if(boss_ugoki == 6) {
						//  動き方  四角形左回り
						boss_jyoutai = 700;
						boss_vy = boss_speed*(-1);			//  ボスの速度
						boss_vx = boss_speed*(-1);			//  ボスの速度
					}
					else if(boss_ugoki == 7) {
						//  動き方  四角形右回り
						boss_jyoutai = 800;
						boss_vy = boss_speed;				//  ボスの速度
						boss_vx = boss_speed*(-1);			//  ボスの速度
					}
					else {
						//  動き方  停止
						boss_jyoutai = 200;
						boss_vx = 0;						//  ボスの速度
					}

					boss_jyoutai_b = boss_jyoutai;			//  状態  バックアップ
				}
			}
			else if(boss_jyoutai == 200) {
				//  動き方  停止

				//  射撃
				shagekiBoss();
			}
			else if(boss_jyoutai == 300) {
				//  動き方  左右移動
				if(boss_vx < 0) {
					//  左へ移動中
					boss_x += boss_vx;
					if(boss_x <= boss_x_shoki-512) {
						//  左端へ到着
						boss_x = boss_x_shoki-512;
						boss_vx = boss_speed;			//  右へ移動
					}
				}
				else {
					//  右へ移動中
					boss_x += boss_vx;
					if(boss_x >= boss_x_shoki-boss_width) {
						//  右端へ到着
						boss_x = boss_x_shoki-boss_width;
						boss_vx = boss_speed*(-1);		//  左へ移動
					}
				}

				//  射撃
				shagekiBoss();
			}
			else if(boss_jyoutai == 400) {
				//  動き方  上下移動
				if(boss_vy < 0) {
					//  上へ移動中
					boss_y += boss_vy;
					if(boss_y <= boss_y_shoki-96) {
						//  上へ到着
						boss_y = boss_y_shoki-96;
						boss_vy = boss_speed;			//  下へ移動
					}
				}
				else {
					//  下へ移動中
					boss_y += boss_vy;
					if(boss_y >= boss_y_shoki+96) {
						//  下へ到着
						boss_y = boss_y_shoki+96;
						boss_vy = boss_speed*(-1);		//  上へ移動
					}
				}

				//  射撃
				shagekiBoss();
			}
			else if(boss_jyoutai == 500) {
				//  動き方  左回転
				boss_kakudo -= boss_speed;
				if(boss_kakudo < 0) boss_kakudo += 360;

				boss_x = (boss_x_shoki-256)+(int)(Math.cos(boss_kakudo*3.14f/180)*96)-32;
				boss_y = (boss_y_shoki+32)+(int)(Math.sin(boss_kakudo*3.14f/180)*96)-32;

				if(boss_kakudo > 180) boss_vx = -1;
				else boss_vx = 1;

				//  射撃
				shagekiBoss();
			}
			else if(boss_jyoutai == 600) {
				//  動き方  右回転
				boss_kakudo += boss_speed;
				if(boss_kakudo >= 360) boss_kakudo -= 360;

				boss_x = (boss_x_shoki-256)+(int)(Math.cos(boss_kakudo*3.14f/180)*96)-32;
				boss_y = (boss_y_shoki+32)+(int)(Math.sin(boss_kakudo*3.14f/180)*96)-32;

				if(boss_kakudo < 180) boss_vx = -1;
				else boss_vx = 1;

				//  射撃
				shagekiBoss();
			}
			else if(boss_jyoutai == 700) {
				//  動き方  四角形左回り
				if(boss_vx < 0) {
					//  左向き
					if(boss_y > boss_y_shoki-64) {
						//  上へ到達していない

						//  上へ移動
						boss_y -= boss_speed;
						if(boss_y <= boss_y_shoki-64) {
							//  上へ到着
							boss_y = boss_y_shoki-64;
							boss_vy = 0;
						}
					}
					else {
						//  上へ到達していたので左へ移動
						boss_x += boss_vx;
						if(boss_x <= boss_x_shoki-512+128-boss_width) {
							//  左端へ到着
							boss_x = boss_x_shoki-512+128-boss_width;

							//  右へ移動
							boss_vx = boss_speed;
							boss_vy = boss_speed;
						}
					}
				}
				else {
					//  右向き
					if(boss_y < boss_y_shoki+64) {
						//  下へ到達していない

						//  下へ移動
						boss_y += boss_speed;
						if(boss_y >= boss_y_shoki+64) {
							//  下へ到着
							boss_y = boss_y_shoki+64;
							boss_vy = 0;
						}
					}
					else {
						//  下へ到達していたので右へ移動
						boss_x += boss_vx;
						if(boss_x >= boss_x_shoki-128) {
							//  右端へ到着
							boss_x = boss_x_shoki-128;

							//  左へ移動
							boss_vx = boss_speed*(-1);
							boss_vy = boss_speed*(-1);
						}
					}
				}

				//  射撃
				shagekiBoss();
			}
			else if(boss_jyoutai == 800) {
				//  動き方  四角形右回り
				if(boss_vx < 0) {
					//  左向き
					if(boss_y < boss_y_shoki+64) {
						//  下へ到達していない

						//  下へ移動
						boss_y += boss_speed;
						if(boss_y >= boss_y_shoki+64) {
							//  下へ到着
							boss_y = boss_y_shoki+64;
							boss_vy = 0;
						}
					}
					else {
						//  下へ到達していたので左へ移動
						boss_x += boss_vx;
						if(boss_x <= boss_x_shoki-512+128-boss_width) {
							//  左端へ到着
							boss_x = boss_x_shoki-512+128-boss_width;

							//  右へ移動
							boss_vx = boss_speed;
							boss_vy = boss_speed*(-1);
						}
					}
				}
				else {
					//  右向き
					if(boss_y > boss_y_shoki-64) {
						//  上へ到達していない

						//  上へ移動
						boss_y -= boss_speed;
						if(boss_y <= boss_y_shoki-64) {
							//  上へ到着
							boss_y = boss_y_shoki-64;
							boss_vy = 0;
						}
					}
					else {
						//  上へ到達していたので右へ移動
						boss_x += boss_vx;
						if(boss_x >= boss_x_shoki-128) {
							//  右端へ到着
							boss_x = boss_x_shoki-128;

							//  左へ移動
							boss_vx = boss_speed*(-1);
							boss_vy = boss_speed;
						}
					}
				}

				//  射撃
				shagekiBoss();
			}


			//  ボスと主人公との当たり判定
			if(boss_fumeru_f == 3) {
				//  重なっても何も起きない設定
			}
			else if(Applet1.getMyXReal()+24 > boss_x  &&  Applet1.getMyXReal() < boss_x+(boss_width-32)+24*2) {
				if(Applet1.getMyYReal()+24 > boss_y  &&  Applet1.getMyYReal() < boss_y+(boss_height-32)+24*2) {
					//  主人公と重なった

					//  踏み潰し
					if(boss_fumeru_f == 2  &&  Applet1.getMyVY() > 10) {
						//  主人公が踏む
						Applet1.setMyPress("3");
						Applet1.setMyYReal(Integer.toString(boss_y));

						//  ボスのＨＰを減らす
						boss_hp = boss_hp-1;

						if(boss_hp <= 0) {
							//  ボスのＨＰが無くなった
							boss_hp = 0;

							//  ボスの状態
							boss_jyoutai = 50;	//  爆発中
							boss_bc = 20;		//  爆発カウンター
						}
						else {
							//  ボスが潰れる

							//  ボスの状態
							boss_jyoutai = 80;	//  潰れ中
							boss_bc = 10;		//  爆発カウンター
						}
					}
					else {
						if(boss_fumeru_f == 4) {
							//  主人公が即死する設定
							Applet1.setMyMiss("2");
						}
						else {
							//  主人公の HP を減らす
							Applet1.setMyHPDamage("1");
							if(Applet1.getMyHP() <= 0) {
								//  HP がなくなったので主人公死亡
								Applet1.setMyMiss("2");
							}
						}
					}
				}
			}

			//  ボスとファイヤーボールとの当たり判定
			int atari = Applet1.attackFire(Integer.toString(boss_x-24),Integer.toString(boss_y-24),Integer.toString((boss_width-32)+24*2),Integer.toString((boss_height-32)+24*2));
			//  ボスに命中した
			if(atari >= 1) {
				//  ボスのＨＰを減らす
				boss_hp = boss_hp-atari;
				if(boss_hp <= 0) {
					//  ＨＰが無くなった
					boss_hp = 0;

					//  ボスの状態
					boss_jyoutai = 50;	//  爆発中
					boss_bc = 20;		//  爆発カウンター
				}
			}

			//  主人公のしっぽ攻撃
			if(boss_tail_f == 2) {
				//  しっぽ攻撃有効
				if(mc.mp.j_tail_ac == 5) {
					//  しっぽのアニメーションが当たり判定あり

					n = 0;				//  当たりフラグ
					if(mc.mp.co_j.y >= boss_y+boss_height-4  ||  mc.mp.co_j.y+32 <= boss_y+4) {
						//  Ｙ座標がズレているのでハズレ
					}
					else if(mc.mp.co_j.muki == 0) {
						//  主人公が左向き
						if(mc.mp.co_j.x-32-12 <= boss_x+boss_width  &&  mc.mp.co_j.x+8 >= boss_x) {
							n = 1;
						}
					}
					else {
						//  主人公が右向き
						if(mc.mp.co_j.x+32+32+12 >= boss_x  &&  mc.mp.co_j.x+24 <= boss_x+boss_width) {
							n = 1;
						}
					}

					if(n == 1) {
						//  しっぽがボスに当たった
						mc.mp.gs.rsAddSound(9);	//  サウンド  敵を飛ばす

						//  ボスのＨＰを減らす
						boss_hp = boss_hp-1;
						if(boss_hp <= 0) {
							//  ＨＰが無くなった
							boss_hp = 0;

							//  ボスの状態
							boss_jyoutai = 50;	//  爆発中
							boss_bc = 20;		//  爆発カウンター
						}
					}
				}
			}
		}

		//  ボスを表示する
		if(view_x >= sl_x) {
			if(boss_jyoutai >= 100) {
				//  戦闘中
				if(boss_vx <= 0) {
					//  左向き
					if(boss_ac <= 2) {
						Offscreen_g.drawImage(boss_left1_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
					else {
						Offscreen_g.drawImage(boss_left2_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
				}
				else {
					//  右向き
					if(boss_ac <= 2) {
						Offscreen_g.drawImage(boss_right1_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
					else {
						Offscreen_g.drawImage(boss_right2_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
				}
			}
			else if(boss_jyoutai >= 50  &&  boss_jyoutai <= 80) {
				//  爆発中  潰れ中
				if(boss_vx <= 0) {
					//  左向き
					Offscreen_g.drawImage(boss_tubure_left_img,boss_x-view_x,boss_y-view_y,Applet1);
				}
				else {
					//  右向き
					Offscreen_g.drawImage(boss_tubure_right_img,boss_x-view_x,boss_y-view_y,Applet1);
				}
			}

			//  アニメーションカウンター
			boss_ac++;
			if(boss_ac > 5) boss_ac = 0;
		}

		//  ボスのＨＰゲージを表示する
		if(view_x >= sl_x  &&  boss_jyoutai >= 50) {
			Applet1.showGauge(Integer.toString((int)(boss_hp*200/boss_hp_max)),boss_name + " " + boss_hp + " / " + boss_hp_max);
		}
	}


	//  ボスが射撃する
	void shagekiBoss() {
		int i,nx,ny;
		double nd;


		//  ボスの技  なし
		if(boss_waza == 1) return;

		if(boss_shageki_c > 0) {
			//  射撃準備中
			boss_shageki_c--;

			if(boss_shageki_c <= 0) {
				//  射撃準備完了

				//  射撃カウンター２初期化
				boss_shageki_c2 = 0;
			}
		}

		if(boss_shageki_c <= 0) {
			//  射撃中

			//  射撃カウンター２初期化
			boss_shageki_c2++;

			if(boss_waza == 2) {
				//  電撃

				//  音を出さずに電撃発射
				mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),100);

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 3) {
				//  電撃３連射

				if(boss_shageki_c2 == 1  ||  boss_shageki_c2 == 7  ||  boss_shageki_c2 == 13) {
					//  音を出さずに電撃発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),100);
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(10);		//  サウンド  電撃
				}

				if(boss_shageki_c2 >= 13) {
					//  射撃カウンター初期化
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 4) {
				//  はっぱカッター発射

				if(boss_vx <= 0) {
					//  音を出さずに はっぱカッター発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,200);
				}
				else {
					//  音を出さずに はっぱカッター発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,205);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 5) {
				//  はっぱカッター３連射

				if(boss_shageki_c2 == 1  ||  boss_shageki_c2 == 9  ||  boss_shageki_c2 == 17) {
					if(boss_vx <= 0) {
						//  音を出さずに はっぱカッター発射
						mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,200);
					}
					else {
						//  音を出さずに はっぱカッター発射
						mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,205);
					}
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(11);		//  サウンド  はっぱカッター
				}

				if(boss_shageki_c2 >= 17) {
					//  射撃カウンター初期化
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 6) {
				//  火の粉

				//  音を出さずに火の粉発射
				if(boss_vx <= 0) {
					mc.mp.mSet(boss_x+32,boss_y+(int)((boss_height-32)/2),300);
				}
				else {
					mc.mp.mSet(boss_x+boss_width-32,boss_y+(int)((boss_height-32)/2),305);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 7) {
				//  火の粉２連射

				//  音を出さずに火の粉発射
				if(boss_vx <= 0) {
					mc.mp.mSet(boss_x+32,boss_y+(int)((boss_height-32)/2)-16,300);
					mc.mp.mSet(boss_x+32,boss_y+(int)((boss_height-32)/2)+16,300);
				}
				else {
					mc.mp.mSet(boss_x+boss_width-32,boss_y+(int)((boss_height-32)/2)-16,305);
					mc.mp.mSet(boss_x+boss_width-32,boss_y+(int)((boss_height-32)/2)+16,305);
				}

				mc.mp.gs.rsAddSound(14);			//  サウンド  火の粉

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 8) {
				//  水鉄砲発射

				if(boss_vx <= 0) {
					//  音を出さずに水鉄砲発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,400);
				}
				else {
					//  音を出さずに水鉄砲発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,405);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 9) {
				//  爆撃発射

				if(boss_vx == 0) {
					//  音を出さずに爆撃発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+boss_height-16,606);
				}
				else if(boss_vx < 0) {
					//  音を出さずに爆撃発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+boss_height-16,600);
				}
				else {
					//  音を出さずに爆撃発射
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+boss_height-16,605);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 10) {
				//  バブル光線  扇型に発射

				if(boss_shageki_c2 == 1) {
					if(boss_vx <= 0) {
						nd = 190*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 170*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 210*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 150*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線
					}
					else {
						nd = 190*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 170*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 210*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 150*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線
					}
				}
				else if(boss_shageki_c2 == 11) {
					if(boss_vx <= 0) {
						nd = 180*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 155*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 205*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線
					}
					else {
						nd = 180*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 155*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線

						nd = 205*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線
					}
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(18);		//  サウンド  みずのはどう
				}

				if(boss_shageki_c2 >= 11) {
					//  射撃カウンター初期化
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 11) {
				//  バブル光線  放射状に発射

				if(boss_shageki_c2 == 1) {
					for(i = 0;i <= 330;i += 30) {
						nd = i*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線
					}
				}
				else if(boss_shageki_c2 == 11) {
					for(i = 15;i <= 345;i += 30) {
						nd = i*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  バブル光線
					}
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(18);		//  サウンド  みずのはどう
				}

				if(boss_shageki_c2 >= 11) {
					//  射撃カウンター初期化
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 12) {
				//  前へ亀を投げる
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,-4);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,4);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 13) {
				//  後へ亀を投げる
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,2);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,-2);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 14) {
				//  前へヒノララシを投げる
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,-6);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,6);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 15) {
				//  後へヒノララシを投げる
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,2);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,-2);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 16) {
				//  前へマリリを投げる
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,-5);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,5);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 17) {
				//  後へマリリを投げる
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,2);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,-2);
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 18) {
				//  アイシクルフォール
				for(i = 0;i <= 300;i += 60) {
					//  アイシクルフォール  右回り
					mc.mp.mSet2(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),900,i,0);
					//  アイシクルフォール  左回り
					mc.mp.mSet2(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),910,300-i,0);

					mc.mp.gs.rsAddSound(18);		//  サウンド  みずのはどう
				}

				//  射撃カウンター初期化
				boss_shageki_c = boss_waza_wait;
			}
		}
	}


	//  Ｘ座標指定画像  初期化
	void initXImage() {
		int i,n;


		for(i = 0;i <= 3;i++) {
			ximage_c[i] = 0;			//  Ｘ座標指定画像		カウンター
		}

		//  Ｘ座標指定画像１
		n = getParamInt("ximage1_view_x");
		if(n > 0) {
			ximage_view_x[0] = (n+1)*32;
			ximage_x[0] = getParamInt("ximage1_x");
			ximage_y[0] = getParamInt("ximage1_y");
			ximage_c[0] = 100;
			ximage_img[0] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage1"));
		}

		//  Ｘ座標指定画像２
		n = getParamInt("ximage2_view_x");
		if(n > 0) {
			ximage_view_x[1] = (n+1)*32;
			ximage_x[1] = getParamInt("ximage2_x");
			ximage_y[1] = getParamInt("ximage2_y");
			ximage_c[1] = 100;
			ximage_img[1] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage2"));
		}

		//  Ｘ座標指定画像３
		n = getParamInt("ximage3_view_x");
		if(n > 0) {
			ximage_view_x[2] = (n+1)*32;
			ximage_x[2] = getParamInt("ximage3_x");
			ximage_y[2] = getParamInt("ximage3_y");
			ximage_c[2] = 100;
			ximage_img[2] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage3"));
		}

		//  Ｘ座標指定画像４
		n = getParamInt("ximage4_view_x");
		if(n > 0) {
			ximage_view_x[3] = (n+1)*32;
			ximage_x[3] = getParamInt("ximage4_x");
			ximage_y[3] = getParamInt("ximage4_y");
			ximage_c[3] = 100;
			ximage_img[3] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage4"));
		}
	}


	//  Ｘ座標指定画像  動く
	void moveXImage(Graphics Offscreen_g,int view_x) {
		int i;


		for(i = 0;i <= 3;i++) {
			if(ximage_c[i] <= 0) {
				//  なし
				continue;
			}
			else if(ximage_c[i] >= 100) {
				//  待機中
				if(view_x >= ximage_view_x[i]) {
					//  表示開始
					ximage_c[i]--;
				}
			}

			if(ximage_c[i] < 100) {
				//  表示中
				Offscreen_g.drawImage(ximage_img[i],ximage_x[i],ximage_y[i],Applet1);

				//  カウントダウン
				ximage_c[i]--;
			}
		}
	}
}
