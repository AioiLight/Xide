/**
		�܂����R���X�g���N�V���� �e�w

		�ȈՐݒ�A�v���b�g
*/

//  �C���|�[�g
import java.applet.*;
import java.awt.*;
import java.awt.event.*;


/**  �ȈՐݒ�A�v���b�g			*/
public class MasaoKani extends MasaoFXApplet {
	MasaoKani Applet1;			//  ���̃N���X    JavaScript ����̈ڐA�p

	//  ���[�U�[  �O���[�o���ϐ�
	int boss_v;					//  �{�X�̗L��
	int boss_x;					//  �{�X�̂w���W
	int boss_y;					//  �{�X�̂x���W
	int boss_width;				//  �{�X�̂w���W
	int boss_height;			//  �{�X�̂x���W
	int boss_vx;				//  �w�����̑��x
	int boss_vy;				//  �x�����̑��x
	int boss_kakudo;			//  �{�X�̊p�x
	int boss_x_shoki;			//  �{�X�̏����w���W
	int boss_y_shoki;			//  �{�X�̏����x���W
	int boss_speed;				//  �{�X�̍ō����x
	int boss_hp;				//  �{�X�̂g�o
	int boss_hp_max;			//  �{�X�̍ő�g�o
	int boss_jyoutai;			//  �{�X�̏��
	int boss_jyoutai_b;			//  �{�X�̏�ԃo�b�N�A�b�v�p
	int boss_bc;				//  �{�X�̔����J�E���^�[
	int boss_ac;				//  �{�X�̃A�j���[�V�����J�E���^�[
	int boss_shageki_c;			//  �{�X�̎ˌ��J�E���^�[
	int boss_shageki_c2;		//  �{�X�̎ˌ��J�E���^�[�Q
	String boss_name;			//  �{�X�̖��O
	int boss_ugoki;				//  �{�X�̓�����
	int boss_waza;				//  �{�X�̋Z
	int boss_waza_wait;			//  �{�X�̋Z�g�p��̑ҋ@����
	int boss_fumeru_f;			//  ���߂邩�t���O
	int boss_tail_f;			//  �����ۍU���L���t���O
	int boss_destroy;			//  �{�X��|�����ꍇ
	Image boss_left1_img;		//  �{�X�̍������摜
	Image boss_right1_img;		//  �{�X�̉E�����摜
	Image boss_left2_img;		//  �{�X�̍������摜
	Image boss_right2_img;		//  �{�X�̉E�����摜
	Image boss_tubure_left_img;	//  �{�X�ׂ̒ꂽ�������摜
	Image boss_tubure_right_img;//  �{�X�ׂ̒ꂽ�E�����摜
	int sl_x;					//  �X�N���[�����b�N����w���W
	int bgm_switch;				//  �a�f�l���t		1  ���Ȃ�
								//					2  ����
								//					3  ���[�v���t
	int bgm_gen = 0;			//  ���݉��t���̋�
	AudioClip[] bgm = new AudioClip[10];
								//  �a�f�l  0	�Ȃ�
								//			1	�X�e�[�W�P
								//			2	�X�e�[�W�Q
								//			3	�X�e�[�W�R
								//			4	�X�e�[�W�S
								//			5	�{�X
								//			6	�^�C�g�����
								//			7	�G���f�B���O
								//			8	�n�}���
	String[] bgm_filename = new String[10];

	int[] ximage_view_x = new int[4];		//  �w���W�w��摜		�\���J�n�w���W
	int[] ximage_x = new int[4];			//  �w���W�w��摜		�\���w���W
	int[] ximage_y = new int[4];			//  �w���W�w��摜		�\���x���W
	int[] ximage_c = new int[4];			//  �w���W�w��摜		�J�E���^�[
	Image[] ximage_img = new Image[4];		//  �w���W�w��摜		�摜


	//**  �A�v���b�g    ap
	//  ������    ap
	public void init() {


		//  �܂����R���X�g���N�V�����z�u
		mc = new MasaoGhost(this,this);


		//  ���[�U�[�������G���A  ��������

		//  ���[�U�[�������p
		userInitJS();

		//  ���[�U�[�������G���A  �����܂�


	}


	//  �`��    ap
	public void paint(Graphics g){
		String nst;


		//  �Q�[����ʂ�`��
		if(my_offscreen_img != null) {
			g.drawImage(my_offscreen_img,0,0,this);
		}
		else {
			//  �N�����̃��b�Z�[�W

			//  �h��Ԃ�
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


	//  ��~    ap
	public void stop() {


		//  �܂����R���X�g���N�V������~
		mc.stop();

		//  �a�f�l��~
		if(bgm_switch == 2  ||  bgm_switch == 3) {
			//  �a�f�l�g�p��

			//  ���ݍĐ����̋Ȃ��~
			if(bgm[bgm_gen] != null) {
				bgm[bgm_gen].stop();
			}
		}
	}


	//  �܂����C�x���g
	//  �`�撼�O�Ƀ��C���v���O��������Ăяo�����
	//
	//  Graphics offscreen_g;			//  �I�t�X�N���[��
	//  Image offscreen_img;			//  �I�t�X�N���[��
	public void masaoEvent(Graphics offscreen_g,Image offscreen_img) {


		//  ���[�U�[�v���O�����G���A  ��������

		int mode;


		//  ���[�h���擾
		mode = Applet1.getMode();

		if(mode == 1) {
			//  �^�C�g�����

			//  JavaScript �^�C�g����ʕ\�������Ăяo��
			userTitleJS(offscreen_g);
		}
		else if(mode >= 100  &&  mode < 200) {
			//  �Q�[����
			if(Applet1.getJSMes() == 1) {
				//  �Q�[���J�n

				//  JavaApplet ����̃��b�Z�[�W���N���A�[
				Applet1.setJSMes("2");

				//  �Q�[���J�n���Ăяo��
				userGameStartJS();
			}
			else {
				//  �Q�[�������Ăяo��
				userGameJS(offscreen_g,Applet1.getViewXReal(),Applet1.getViewYReal());
			}
		}
		else if(mode == 200) {
			//  �Q�[���I�[�o�[
			userGameoverJS(offscreen_g);
		}
		else if(mode == 300) {
			//  �G���f�B���O
			userEndingJS(offscreen_g);
		}
		else if(mode == 400) {
			//  �n�}���
			userChizuJS(offscreen_g);
		}

		//  ���[�U�[�v���O�����G���A  �����܂�


		//  �Q�[����ʂ̃C���[�W���擾
		my_offscreen_img = offscreen_img;

		//  �ĕ`��
		repaint();
	}




	//  ���[�U�[���\�b�h
	//
	//  ���\�b�h���� MasaoXJSS2.class �p�Ɠ���

	//  JavaApplet �N������ Java ����P�񂾂��Ăяo�����
	void userInitJS() {


		//  ���̃N���X���擾
		Applet1 = this;

		//  �a�f�l�̏�����
		initBGM();
	}


	//  �^�C�g����ʕ\������ Java ����Ăяo�����
	void userTitleJS(Graphics Offscreen_g) {


		//  �a�f�l  �^�C�g�����
		playBGM(6);
	}


	//  �Q�[���J�n���� Java ����P�񂾂��Ăяo�����
	void userGameStartJS() {
		int n;
		String nst;


		//  ��l���̂g�o�ݒ�
		n = getParamInt("j_hp");
		if(n < 1) n = 1;
		if(n > 1) {
			Applet1.setMyMaxHP(Integer.toString(n));

			//  ��l���̂g�o�\��
			nst = getParameter("j_hp_name");
			if(nst == null) nst = "";
			Applet1.showMyHP(nst);
		}

		//  ��l���̃t�@�C���[�{�[���W������
		n = getParamInt("j_equip_fire");
		if(n == 2) {
			Applet1.equipFire();
		}

		//  ��l���̃O���l�[�h������
		n = getParamInt("j_equip_grenade");
		if(n >= 1) {
			Applet1.equipGrenade(Integer.toString(n));
		}

		//  �w���W�w��摜  ������
		initXImage();

		//  �{�X������������
		initBoss();

		//  �a�f�l�Đ�
		n = Applet1.getMode();
		if(n == 102) {
			//  �X�e�[�W�Q
			playBGM(2);
		}
		else if(n == 103) {
			//  �X�e�[�W�R
			playBGM(3);
		}
		else if(n == 104) {
			//  �X�e�[�W�S
			playBGM(4);
		}
		else {
			//  �X�e�[�W�P
			playBGM(1);
		}
	}


	//  �Q�[����    �`�撼�O�� Java ����Ăяo�����
	void userGameJS(Graphics Offscreen_g,int view_x,int view_y) {


		//  �{�X�𓮂���
		if(boss_jyoutai > 0) {
			moveBoss(Offscreen_g,view_x,view_y);
		}

		//  �w���W�w��摜�𓮂���
		moveXImage(Offscreen_g,view_x);

		if(Applet1.getMode() == 150) {
			//  �a�f�l�Đ�  �{�X
			playBGM(5);
		}
	}


	//  �Q�[���I�[�o�[    �`�撼�O�� Java ����Ăяo�����
	void userGameoverJS(Graphics Offscreen_g) {


			//  �a�f�l�Đ�  �Q�[���I�[�o�[
			playBGM(0);
	}


	//  �G���f�B���O    �`�撼�O�� Java ����Ăяo�����
	void userEndingJS(Graphics Offscreen_g) {


			//  �a�f�l�Đ�  �G���f�B���O
			playBGM(7);
	}


	//  �n�}���    �`�撼�O�� Java ����Ăяo�����
	void userChizuJS(Graphics Offscreen_g) {


			//  �a�f�l�Đ�  �n�}���
			playBGM(8);
	}


	//  param �^�O���� value �� int �^�Ŏ擾����
	//  String st1;		//  name ����
	//  �߂�l int		//  �G���[�̏ꍇ�� -1
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


	//  �a�f�l������������
	void initBGM() {
		int i,n;


		//  ������
		for(i = 0;i <= 9;i++) {
			bgm_filename[i] = null;
			bgm[i] = null;
		}

		//  �a�f�l�̃��[�v�Đ�
		bgm_switch = getParamInt("bgm_switch");
		if(bgm_switch < 1  ||  bgm_switch > 3) bgm_switch = 1;

		if(bgm_switch == 2  ||  bgm_switch == 3) {
			//  �t�@�C�������擾
			bgm_filename[1] = getParameter("filename_bgm_stage1");
			bgm_filename[2] = getParameter("filename_bgm_stage2");
			bgm_filename[3] = getParameter("filename_bgm_stage3");
			bgm_filename[4] = getParameter("filename_bgm_stage4");
			bgm_filename[5] = getParameter("filename_bgm_boss");
			bgm_filename[6] = getParameter("filename_bgm_title");
			bgm_filename[7] = getParameter("filename_bgm_ending");
			bgm_filename[8] = getParameter("filename_bgm_chizu");

			//  AudioClip ���擾
			for(i = 1;i <= 8;i++) {
				if(bgm_filename[i] != null) {
					if(bgm_filename[i].length() > 3) {
						bgm[i] = getAudioClip(getDocumentBase(),bgm_filename[i]);
					}
				}
			}
		}
	}


	//  �a�f�l���Đ�����
	//  int k;				//  �ȃR�[�h
	void playBGM(int k) {


		//  �Đ����Ȃ�
		if(bgm_switch == 1) return;

		if(bgm_gen != k) {
			//  ���ݍĐ����̋ȂƈႤ
			if(bgm_filename[bgm_gen] != null  &&  bgm_filename[k] != null) {
				if(bgm_filename[bgm_gen].equals(bgm_filename[k])  == true) {
					//  �Đ��\��̋Ȃƃt�@�C����������

					//  ���݂̋Ȃ�ݒ�
					bgm_gen = k;

					//  �����I��
					return;
				}
			}

			//  ���ݍĐ����̋Ȃ��~
			if(bgm[bgm_gen] != null) {
				bgm[bgm_gen].stop();
			}

			//  ���݂̋Ȃ�ݒ�
			bgm_gen = k;

			//  �Ȃ��Đ�
			if(bgm[bgm_gen] != null) {
				if(bgm_switch == 3) {
					//  ���[�v�Đ�
					bgm[bgm_gen].loop();
				}
				else {
					//  �Đ�
					bgm[bgm_gen].play();
				}
			}
		}
	}


	//  �{�X������������
	void initBoss() {
		int n;
		String nst;


		//  param �^�O�Ń{�X�̔\�͂�ݒ�

		//  ���
		boss_v = getParamInt("oriboss_v");
		if(boss_v >= 2) {
			//  �{�X��z�u����

			boss_jyoutai = 30;					//  ���  �ҋ@��
			boss_jyoutai_b = boss_jyoutai;		//  ���  �o�b�N�A�b�v
			boss_ac = 0;						//  �A�j���[�V�����J�E���^�[

			//  ���O
			boss_name = getParameter("oriboss_name");
			if(boss_name == null) boss_name = "";

			//  HP
			boss_hp = getParamInt("oriboss_hp");
			if(boss_hp < 1) boss_hp = 1;
			boss_hp_max = boss_hp;

			if(boss_v == 3) {
				//  �Z���N�E�U�̕��� Z �̈ʒu�ɐݒu
				if(mc.mp.co_b.c == 300) {
					//  �Z���N�E�U�z�u��
					mc.mp.co_b.c = 0;		//  �Z���N�E�U�P��

					//  ���W��ݒ�
					boss_x = mc.mp.co_b.x;
					boss_y = mc.mp.co_b.y+16;
				}
				else {
					//  �{�X��z�u���Ȃ�
					boss_jyoutai = 0;
				}
			}
			else {
				//  ���W�w��Őݒu

				//  �w���W
				boss_x = getParamInt("oriboss_x");
				if(boss_x < 0) boss_x = 0;
				if(boss_x > 179) boss_x = 179;
				boss_x = (boss_x+1)*32;

				//  �x���W
				boss_y = getParamInt("oriboss_y");
				if(boss_y < 0) boss_y = 0;
				if(boss_y > 29) boss_y = 29;
				boss_y = (boss_y+10)*32;
			}

			//  �������W
			boss_x_shoki = boss_x;
			boss_y_shoki = boss_y;

			//  ��
			boss_width = getParamInt("oriboss_width");
			if(boss_width < 32) boss_width = 32;

			//  ��
			boss_height = getParamInt("oriboss_height");
			if(boss_height < 32) boss_height = 32;

			//  ���x
			boss_speed = getParamInt("oriboss_speed");
			if(boss_speed < 1) boss_speed = 1;
			//  boss_vx = boss_speed*(-1);
			boss_vx = -4;

			//  ������
			boss_ugoki = getParamInt("oriboss_ugoki");
			if(boss_ugoki < 1) boss_ugoki = 1;
			if(boss_ugoki > 7) boss_ugoki = 7;

			//  �Z
			boss_waza = getParamInt("oriboss_waza");
			if(boss_waza < 0) boss_waza = 1;
			if(boss_waza > 18) boss_waza = 18;

			//  �Z�g�p��̑ҋ@����
			boss_waza_wait = getParamInt("oriboss_waza_wait");
			if(boss_waza_wait < 1) boss_waza_wait = 1;

			//  ���߂邩�̃t���O
			boss_fumeru_f = getParamInt("oriboss_fumeru_f");
			if(boss_fumeru_f < 1  ||  boss_fumeru_f > 4) boss_fumeru_f = 1;

			//  �����ۍU���L���t���O
			boss_tail_f = getParamInt("oriboss_tail_f");
			if(boss_tail_f != 2) boss_tail_f = 1;

			//  �{�X��|�����ꍇ
			boss_destroy = getParamInt("oriboss_destroy");
			if(boss_destroy != 2) boss_destroy = 1;

			if(boss_jyoutai > 0) {
				//  �{�X���z�u����Ă���

				//  �{�X�̉摜���擾
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
					mt.waitForID(0);			//  �ǂݍ��ނ܂ő҂�
				}
				catch (InterruptedException e) {
				}

				//  �X�N���[�����b�N��ݒ�
				sl_x = boss_x-512;
				Applet1.setScrollLock(Integer.toString(sl_x));

				//  �ˌ��J�E���^�[
				//  boss_shageki_c = boss_waza_wait;
				boss_shageki_c = 5;
			}
		}
		else {
			//  �{�X��z�u���Ȃ�
			boss_jyoutai = 0;
		}
	}


	//  �{�X�𓮂���
	void moveBoss(Graphics Offscreen_g,int view_x,int view_y) {
		int n;


		//  �{�X�𓮂���
		if(boss_jyoutai == 30) {
			//  �ҋ@��
			if(view_x >= sl_x) {
				//  �X�N���[�����b�N���ꂽ

				//  �o����
				boss_jyoutai = 100;
			}

			//  �����I��
			return;
		}
		else if(boss_jyoutai == 20) {
			//  ��������ԂŃX�e�[�W�N���A�[�ҋ@��
			//  �J�E���g�_�E��
			boss_bc = boss_bc-1;
			if(boss_bc <= 0) {
				//  �X�e�[�W�N���A�[
				Applet1.setStageClear();

				//  �{�X�̏��
				boss_jyoutai = 0;	//  ���S��
			}
		}
		else if(boss_jyoutai == 50) {
			//  ������

			//  �J�E���g�_�E��
			boss_bc = boss_bc-1;
			if(boss_bc <= 0) {
				//  �����I��

				//  �{�X�̏��
				boss_jyoutai = 0;	//  ���S��

				//  �{�X�̂g�o�Q�[�W������
				Applet1.hideGauge();

				//  �P�O�O�O�_���Z
				Applet1.addScore(Integer.toString(1000));

				if(boss_destroy == 2) {
					//  �{�X��|���ƃX�e�[�W�N���A�[
					boss_jyoutai = 20;		//  ��������ԂŃX�e�[�W�N���A�[�ҋ@��
					boss_bc = 30;
				}
				else {
					//  �l�ʐ��o��
					Applet1.setMapchip(Integer.toString(((int)(view_x/32)-1+6)),Integer.toString((int)(view_y/32)-10+4),Integer.toString(8));
				}
			}
		}
		else if(boss_jyoutai == 80) {
			//  �ׂꒆ

			//  �J�E���g�_�E��
			boss_bc = boss_bc-1;
			if(boss_bc <= 0) {
				//  �ׂ��ԏI��

				//  �{�X�̏��
				boss_jyoutai = boss_jyoutai_b;
			}
		}
		else if(boss_jyoutai >= 100) {
			//  �o����

			if(boss_jyoutai == 100) {
				//  ���ֈړ���

				//  �ړ�����
				boss_x = boss_x+boss_vx;

				if(boss_ugoki == 4  ||  boss_ugoki == 5) {
					//  �����A�E���̏ꍇ
					if(boss_x <= sl_x+320) {
						//  �ˌ��ʒu�֗���
						boss_x = sl_x+320;
						boss_vx = 0;

						if(boss_ugoki == 4) {
							//  ������  �����
							boss_jyoutai = 500;
							boss_kakudo = 0;				//  �{�X�̊p�x
						}
						else {
							//  ������  �E���
							boss_jyoutai = 600;
							boss_kakudo = 0;				//  �{�X�̊p�x
						}

						boss_jyoutai_b = boss_jyoutai;		//  ���  �o�b�N�A�b�v
					}
				}
				else if(boss_x <= sl_x+384) {
					//  �ˌ��ʒu�֗���
					boss_x = sl_x+384;
					boss_vx = 0;

					if(boss_ugoki == 2) {
						//  ������  ���E�ړ�
						boss_jyoutai = 300;
						boss_vx = boss_speed*(-1);			//  �{�X�̑��x
					}
					else if(boss_ugoki == 3) {
						//  ������  �㉺�ړ�
						boss_jyoutai = 400;
						boss_vy = boss_speed*(-1);			//  �{�X�̑��x
					}
					else if(boss_ugoki == 6) {
						//  ������  �l�p�`�����
						boss_jyoutai = 700;
						boss_vy = boss_speed*(-1);			//  �{�X�̑��x
						boss_vx = boss_speed*(-1);			//  �{�X�̑��x
					}
					else if(boss_ugoki == 7) {
						//  ������  �l�p�`�E���
						boss_jyoutai = 800;
						boss_vy = boss_speed;				//  �{�X�̑��x
						boss_vx = boss_speed*(-1);			//  �{�X�̑��x
					}
					else {
						//  ������  ��~
						boss_jyoutai = 200;
						boss_vx = 0;						//  �{�X�̑��x
					}

					boss_jyoutai_b = boss_jyoutai;			//  ���  �o�b�N�A�b�v
				}
			}
			else if(boss_jyoutai == 200) {
				//  ������  ��~

				//  �ˌ�
				shagekiBoss();
			}
			else if(boss_jyoutai == 300) {
				//  ������  ���E�ړ�
				if(boss_vx < 0) {
					//  ���ֈړ���
					boss_x += boss_vx;
					if(boss_x <= boss_x_shoki-512) {
						//  ���[�֓���
						boss_x = boss_x_shoki-512;
						boss_vx = boss_speed;			//  �E�ֈړ�
					}
				}
				else {
					//  �E�ֈړ���
					boss_x += boss_vx;
					if(boss_x >= boss_x_shoki-boss_width) {
						//  �E�[�֓���
						boss_x = boss_x_shoki-boss_width;
						boss_vx = boss_speed*(-1);		//  ���ֈړ�
					}
				}

				//  �ˌ�
				shagekiBoss();
			}
			else if(boss_jyoutai == 400) {
				//  ������  �㉺�ړ�
				if(boss_vy < 0) {
					//  ��ֈړ���
					boss_y += boss_vy;
					if(boss_y <= boss_y_shoki-96) {
						//  ��֓���
						boss_y = boss_y_shoki-96;
						boss_vy = boss_speed;			//  ���ֈړ�
					}
				}
				else {
					//  ���ֈړ���
					boss_y += boss_vy;
					if(boss_y >= boss_y_shoki+96) {
						//  ���֓���
						boss_y = boss_y_shoki+96;
						boss_vy = boss_speed*(-1);		//  ��ֈړ�
					}
				}

				//  �ˌ�
				shagekiBoss();
			}
			else if(boss_jyoutai == 500) {
				//  ������  ����]
				boss_kakudo -= boss_speed;
				if(boss_kakudo < 0) boss_kakudo += 360;

				boss_x = (boss_x_shoki-256)+(int)(Math.cos(boss_kakudo*3.14f/180)*96)-32;
				boss_y = (boss_y_shoki+32)+(int)(Math.sin(boss_kakudo*3.14f/180)*96)-32;

				if(boss_kakudo > 180) boss_vx = -1;
				else boss_vx = 1;

				//  �ˌ�
				shagekiBoss();
			}
			else if(boss_jyoutai == 600) {
				//  ������  �E��]
				boss_kakudo += boss_speed;
				if(boss_kakudo >= 360) boss_kakudo -= 360;

				boss_x = (boss_x_shoki-256)+(int)(Math.cos(boss_kakudo*3.14f/180)*96)-32;
				boss_y = (boss_y_shoki+32)+(int)(Math.sin(boss_kakudo*3.14f/180)*96)-32;

				if(boss_kakudo < 180) boss_vx = -1;
				else boss_vx = 1;

				//  �ˌ�
				shagekiBoss();
			}
			else if(boss_jyoutai == 700) {
				//  ������  �l�p�`�����
				if(boss_vx < 0) {
					//  ������
					if(boss_y > boss_y_shoki-64) {
						//  ��֓��B���Ă��Ȃ�

						//  ��ֈړ�
						boss_y -= boss_speed;
						if(boss_y <= boss_y_shoki-64) {
							//  ��֓���
							boss_y = boss_y_shoki-64;
							boss_vy = 0;
						}
					}
					else {
						//  ��֓��B���Ă����̂ō��ֈړ�
						boss_x += boss_vx;
						if(boss_x <= boss_x_shoki-512+128-boss_width) {
							//  ���[�֓���
							boss_x = boss_x_shoki-512+128-boss_width;

							//  �E�ֈړ�
							boss_vx = boss_speed;
							boss_vy = boss_speed;
						}
					}
				}
				else {
					//  �E����
					if(boss_y < boss_y_shoki+64) {
						//  ���֓��B���Ă��Ȃ�

						//  ���ֈړ�
						boss_y += boss_speed;
						if(boss_y >= boss_y_shoki+64) {
							//  ���֓���
							boss_y = boss_y_shoki+64;
							boss_vy = 0;
						}
					}
					else {
						//  ���֓��B���Ă����̂ŉE�ֈړ�
						boss_x += boss_vx;
						if(boss_x >= boss_x_shoki-128) {
							//  �E�[�֓���
							boss_x = boss_x_shoki-128;

							//  ���ֈړ�
							boss_vx = boss_speed*(-1);
							boss_vy = boss_speed*(-1);
						}
					}
				}

				//  �ˌ�
				shagekiBoss();
			}
			else if(boss_jyoutai == 800) {
				//  ������  �l�p�`�E���
				if(boss_vx < 0) {
					//  ������
					if(boss_y < boss_y_shoki+64) {
						//  ���֓��B���Ă��Ȃ�

						//  ���ֈړ�
						boss_y += boss_speed;
						if(boss_y >= boss_y_shoki+64) {
							//  ���֓���
							boss_y = boss_y_shoki+64;
							boss_vy = 0;
						}
					}
					else {
						//  ���֓��B���Ă����̂ō��ֈړ�
						boss_x += boss_vx;
						if(boss_x <= boss_x_shoki-512+128-boss_width) {
							//  ���[�֓���
							boss_x = boss_x_shoki-512+128-boss_width;

							//  �E�ֈړ�
							boss_vx = boss_speed;
							boss_vy = boss_speed*(-1);
						}
					}
				}
				else {
					//  �E����
					if(boss_y > boss_y_shoki-64) {
						//  ��֓��B���Ă��Ȃ�

						//  ��ֈړ�
						boss_y -= boss_speed;
						if(boss_y <= boss_y_shoki-64) {
							//  ��֓���
							boss_y = boss_y_shoki-64;
							boss_vy = 0;
						}
					}
					else {
						//  ��֓��B���Ă����̂ŉE�ֈړ�
						boss_x += boss_vx;
						if(boss_x >= boss_x_shoki-128) {
							//  �E�[�֓���
							boss_x = boss_x_shoki-128;

							//  ���ֈړ�
							boss_vx = boss_speed*(-1);
							boss_vy = boss_speed;
						}
					}
				}

				//  �ˌ�
				shagekiBoss();
			}


			//  �{�X�Ǝ�l���Ƃ̓����蔻��
			if(boss_fumeru_f == 3) {
				//  �d�Ȃ��Ă������N���Ȃ��ݒ�
			}
			else if(Applet1.getMyXReal()+24 > boss_x  &&  Applet1.getMyXReal() < boss_x+(boss_width-32)+24*2) {
				if(Applet1.getMyYReal()+24 > boss_y  &&  Applet1.getMyYReal() < boss_y+(boss_height-32)+24*2) {
					//  ��l���Əd�Ȃ���

					//  ���ݒׂ�
					if(boss_fumeru_f == 2  &&  Applet1.getMyVY() > 10) {
						//  ��l��������
						Applet1.setMyPress("3");
						Applet1.setMyYReal(Integer.toString(boss_y));

						//  �{�X�̂g�o�����炷
						boss_hp = boss_hp-1;

						if(boss_hp <= 0) {
							//  �{�X�̂g�o�������Ȃ���
							boss_hp = 0;

							//  �{�X�̏��
							boss_jyoutai = 50;	//  ������
							boss_bc = 20;		//  �����J�E���^�[
						}
						else {
							//  �{�X���ׂ��

							//  �{�X�̏��
							boss_jyoutai = 80;	//  �ׂꒆ
							boss_bc = 10;		//  �����J�E���^�[
						}
					}
					else {
						if(boss_fumeru_f == 4) {
							//  ��l������������ݒ�
							Applet1.setMyMiss("2");
						}
						else {
							//  ��l���� HP �����炷
							Applet1.setMyHPDamage("1");
							if(Applet1.getMyHP() <= 0) {
								//  HP ���Ȃ��Ȃ����̂Ŏ�l�����S
								Applet1.setMyMiss("2");
							}
						}
					}
				}
			}

			//  �{�X�ƃt�@�C���[�{�[���Ƃ̓����蔻��
			int atari = Applet1.attackFire(Integer.toString(boss_x-24),Integer.toString(boss_y-24),Integer.toString((boss_width-32)+24*2),Integer.toString((boss_height-32)+24*2));
			//  �{�X�ɖ�������
			if(atari >= 1) {
				//  �{�X�̂g�o�����炷
				boss_hp = boss_hp-atari;
				if(boss_hp <= 0) {
					//  �g�o�������Ȃ���
					boss_hp = 0;

					//  �{�X�̏��
					boss_jyoutai = 50;	//  ������
					boss_bc = 20;		//  �����J�E���^�[
				}
			}

			//  ��l���̂����ۍU��
			if(boss_tail_f == 2) {
				//  �����ۍU���L��
				if(mc.mp.j_tail_ac == 5) {
					//  �����ۂ̃A�j���[�V�����������蔻�肠��

					n = 0;				//  ������t���O
					if(mc.mp.co_j.y >= boss_y+boss_height-4  ||  mc.mp.co_j.y+32 <= boss_y+4) {
						//  �x���W���Y���Ă���̂Ńn�Y��
					}
					else if(mc.mp.co_j.muki == 0) {
						//  ��l����������
						if(mc.mp.co_j.x-32-12 <= boss_x+boss_width  &&  mc.mp.co_j.x+8 >= boss_x) {
							n = 1;
						}
					}
					else {
						//  ��l�����E����
						if(mc.mp.co_j.x+32+32+12 >= boss_x  &&  mc.mp.co_j.x+24 <= boss_x+boss_width) {
							n = 1;
						}
					}

					if(n == 1) {
						//  �����ۂ��{�X�ɓ�������
						mc.mp.gs.rsAddSound(9);	//  �T�E���h  �G���΂�

						//  �{�X�̂g�o�����炷
						boss_hp = boss_hp-1;
						if(boss_hp <= 0) {
							//  �g�o�������Ȃ���
							boss_hp = 0;

							//  �{�X�̏��
							boss_jyoutai = 50;	//  ������
							boss_bc = 20;		//  �����J�E���^�[
						}
					}
				}
			}
		}

		//  �{�X��\������
		if(view_x >= sl_x) {
			if(boss_jyoutai >= 100) {
				//  �퓬��
				if(boss_vx <= 0) {
					//  ������
					if(boss_ac <= 2) {
						Offscreen_g.drawImage(boss_left1_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
					else {
						Offscreen_g.drawImage(boss_left2_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
				}
				else {
					//  �E����
					if(boss_ac <= 2) {
						Offscreen_g.drawImage(boss_right1_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
					else {
						Offscreen_g.drawImage(boss_right2_img,boss_x-view_x,boss_y-view_y,Applet1);
					}
				}
			}
			else if(boss_jyoutai >= 50  &&  boss_jyoutai <= 80) {
				//  ������  �ׂꒆ
				if(boss_vx <= 0) {
					//  ������
					Offscreen_g.drawImage(boss_tubure_left_img,boss_x-view_x,boss_y-view_y,Applet1);
				}
				else {
					//  �E����
					Offscreen_g.drawImage(boss_tubure_right_img,boss_x-view_x,boss_y-view_y,Applet1);
				}
			}

			//  �A�j���[�V�����J�E���^�[
			boss_ac++;
			if(boss_ac > 5) boss_ac = 0;
		}

		//  �{�X�̂g�o�Q�[�W��\������
		if(view_x >= sl_x  &&  boss_jyoutai >= 50) {
			Applet1.showGauge(Integer.toString((int)(boss_hp*200/boss_hp_max)),boss_name + " " + boss_hp + " / " + boss_hp_max);
		}
	}


	//  �{�X���ˌ�����
	void shagekiBoss() {
		int i,nx,ny;
		double nd;


		//  �{�X�̋Z  �Ȃ�
		if(boss_waza == 1) return;

		if(boss_shageki_c > 0) {
			//  �ˌ�������
			boss_shageki_c--;

			if(boss_shageki_c <= 0) {
				//  �ˌ���������

				//  �ˌ��J�E���^�[�Q������
				boss_shageki_c2 = 0;
			}
		}

		if(boss_shageki_c <= 0) {
			//  �ˌ���

			//  �ˌ��J�E���^�[�Q������
			boss_shageki_c2++;

			if(boss_waza == 2) {
				//  �d��

				//  �����o�����ɓd������
				mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),100);

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 3) {
				//  �d���R�A��

				if(boss_shageki_c2 == 1  ||  boss_shageki_c2 == 7  ||  boss_shageki_c2 == 13) {
					//  �����o�����ɓd������
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),100);
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(10);		//  �T�E���h  �d��
				}

				if(boss_shageki_c2 >= 13) {
					//  �ˌ��J�E���^�[������
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 4) {
				//  �͂��σJ�b�^�[����

				if(boss_vx <= 0) {
					//  �����o������ �͂��σJ�b�^�[����
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,200);
				}
				else {
					//  �����o������ �͂��σJ�b�^�[����
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,205);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 5) {
				//  �͂��σJ�b�^�[�R�A��

				if(boss_shageki_c2 == 1  ||  boss_shageki_c2 == 9  ||  boss_shageki_c2 == 17) {
					if(boss_vx <= 0) {
						//  �����o������ �͂��σJ�b�^�[����
						mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,200);
					}
					else {
						//  �����o������ �͂��σJ�b�^�[����
						mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,205);
					}
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(11);		//  �T�E���h  �͂��σJ�b�^�[
				}

				if(boss_shageki_c2 >= 17) {
					//  �ˌ��J�E���^�[������
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 6) {
				//  �΂̕�

				//  �����o�����ɉ΂̕�����
				if(boss_vx <= 0) {
					mc.mp.mSet(boss_x+32,boss_y+(int)((boss_height-32)/2),300);
				}
				else {
					mc.mp.mSet(boss_x+boss_width-32,boss_y+(int)((boss_height-32)/2),305);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 7) {
				//  �΂̕��Q�A��

				//  �����o�����ɉ΂̕�����
				if(boss_vx <= 0) {
					mc.mp.mSet(boss_x+32,boss_y+(int)((boss_height-32)/2)-16,300);
					mc.mp.mSet(boss_x+32,boss_y+(int)((boss_height-32)/2)+16,300);
				}
				else {
					mc.mp.mSet(boss_x+boss_width-32,boss_y+(int)((boss_height-32)/2)-16,305);
					mc.mp.mSet(boss_x+boss_width-32,boss_y+(int)((boss_height-32)/2)+16,305);
				}

				mc.mp.gs.rsAddSound(14);			//  �T�E���h  �΂̕�

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 8) {
				//  ���S�C����

				if(boss_vx <= 0) {
					//  �����o�����ɐ��S�C����
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,400);
				}
				else {
					//  �����o�����ɐ��S�C����
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+16,405);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 9) {
				//  ��������

				if(boss_vx == 0) {
					//  �����o�����ɔ�������
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+boss_height-16,606);
				}
				else if(boss_vx < 0) {
					//  �����o�����ɔ�������
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+boss_height-16,600);
				}
				else {
					//  �����o�����ɔ�������
					mc.mp.mSet(boss_x+(int)((boss_width-32)/2),boss_y+boss_height-16,605);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 10) {
				//  �o�u������  ��^�ɔ���

				if(boss_shageki_c2 == 1) {
					if(boss_vx <= 0) {
						nd = 190*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 170*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 210*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 150*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������
					}
					else {
						nd = 190*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 170*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 210*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 150*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������
					}
				}
				else if(boss_shageki_c2 == 11) {
					if(boss_vx <= 0) {
						nd = 180*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 155*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 205*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������
					}
					else {
						nd = 180*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 155*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������

						nd = 205*3.14f/180;
						nx = (int)(Math.cos(nd)*12)*(-1);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+boss_width-48,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������
					}
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(18);		//  �T�E���h  �݂��̂͂ǂ�
				}

				if(boss_shageki_c2 >= 11) {
					//  �ˌ��J�E���^�[������
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 11) {
				//  �o�u������  ���ˏ�ɔ���

				if(boss_shageki_c2 == 1) {
					for(i = 0;i <= 330;i += 30) {
						nd = i*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������
					}
				}
				else if(boss_shageki_c2 == 11) {
					for(i = 15;i <= 345;i += 30) {
						nd = i*3.14f/180;
						nx = (int)(Math.cos(nd)*12);
						ny = (int)(Math.sin(nd)*12)*(-1);
						mc.mp.mSet2(boss_x+16,boss_y+(int)((boss_height-32)/2),710,nx,ny);			//  �o�u������
					}
				}

				if(boss_shageki_c2 == 1) {
					mc.mp.gs.rsAddSound(18);		//  �T�E���h  �݂��̂͂ǂ�
				}

				if(boss_shageki_c2 >= 11) {
					//  �ˌ��J�E���^�[������
					boss_shageki_c = boss_waza_wait;
				}
			}
			else if(boss_waza == 12) {
				//  �O�֋T�𓊂���
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,-4);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,4);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 13) {
				//  ��֋T�𓊂���
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,2);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,150,-2);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 14) {
				//  �O�փq�m�����V�𓊂���
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,-6);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,6);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 15) {
				//  ��փq�m�����V�𓊂���
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,2);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,450,-2);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 16) {
				//  �O�փ}�����𓊂���
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,-5);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,5);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 17) {
				//  ��փ}�����𓊂���
				if(boss_vx <= 0) {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,2);
				}
				else {
					mc.mp.tSetBoss(boss_x+(int)((boss_width-32)/2),boss_y,650,-2);
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
			else if(boss_waza == 18) {
				//  �A�C�V�N���t�H�[��
				for(i = 0;i <= 300;i += 60) {
					//  �A�C�V�N���t�H�[��  �E���
					mc.mp.mSet2(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),900,i,0);
					//  �A�C�V�N���t�H�[��  �����
					mc.mp.mSet2(boss_x+(int)((boss_width-32)/2),boss_y+(int)((boss_height-32)/2),910,300-i,0);

					mc.mp.gs.rsAddSound(18);		//  �T�E���h  �݂��̂͂ǂ�
				}

				//  �ˌ��J�E���^�[������
				boss_shageki_c = boss_waza_wait;
			}
		}
	}


	//  �w���W�w��摜  ������
	void initXImage() {
		int i,n;


		for(i = 0;i <= 3;i++) {
			ximage_c[i] = 0;			//  �w���W�w��摜		�J�E���^�[
		}

		//  �w���W�w��摜�P
		n = getParamInt("ximage1_view_x");
		if(n > 0) {
			ximage_view_x[0] = (n+1)*32;
			ximage_x[0] = getParamInt("ximage1_x");
			ximage_y[0] = getParamInt("ximage1_y");
			ximage_c[0] = 100;
			ximage_img[0] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage1"));
		}

		//  �w���W�w��摜�Q
		n = getParamInt("ximage2_view_x");
		if(n > 0) {
			ximage_view_x[1] = (n+1)*32;
			ximage_x[1] = getParamInt("ximage2_x");
			ximage_y[1] = getParamInt("ximage2_y");
			ximage_c[1] = 100;
			ximage_img[1] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage2"));
		}

		//  �w���W�w��摜�R
		n = getParamInt("ximage3_view_x");
		if(n > 0) {
			ximage_view_x[2] = (n+1)*32;
			ximage_x[2] = getParamInt("ximage3_x");
			ximage_y[2] = getParamInt("ximage3_y");
			ximage_c[2] = 100;
			ximage_img[2] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage3"));
		}

		//  �w���W�w��摜�S
		n = getParamInt("ximage4_view_x");
		if(n > 0) {
			ximage_view_x[3] = (n+1)*32;
			ximage_x[3] = getParamInt("ximage4_x");
			ximage_y[3] = getParamInt("ximage4_y");
			ximage_c[3] = 100;
			ximage_img[3] = Applet1.getImage(Applet1.getDocumentBase(),getParameter("filename_ximage4"));
		}
	}


	//  �w���W�w��摜  ����
	void moveXImage(Graphics Offscreen_g,int view_x) {
		int i;


		for(i = 0;i <= 3;i++) {
			if(ximage_c[i] <= 0) {
				//  �Ȃ�
				continue;
			}
			else if(ximage_c[i] >= 100) {
				//  �ҋ@��
				if(view_x >= ximage_view_x[i]) {
					//  �\���J�n
					ximage_c[i]--;
				}
			}

			if(ximage_c[i] < 100) {
				//  �\����
				Offscreen_g.drawImage(ximage_img[i],ximage_x[i],ximage_y[i],Applet1);

				//  �J�E���g�_�E��
				ximage_c[i]--;
			}
		}
	}
}
