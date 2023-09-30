# PapagoTranslator-PowerToys

![introduce](https://github.com/patcher454/PapagoTranslator-PowerToys/assets/34996184/e8896240-33b6-47f7-b522-f9143b40f67c)

## 사용 방법
해당 플러그인은 PowerToys가 필요합니다.

[PowerToys 다운로드](https://learn.microsoft.com/ko-kr/windows/powertoys/install)

또한 네이버 개발자 사이트에서 애플리케이션 등록이 필요합니다 (무료).

[네이버 개발자 애플리케이션 등록](https://developers.naver.com/apps/#/register)

![image](https://github.com/patcher454/PapagoTranslator-PowerToys/assets/34996184/4de0df90-3d90-46b2-8706-d71386d510e6)

**사용 API는 꼭 Papago 번역과 언어감지를 추가해주세요!**

PowerToys 설정 > PowerToys Run > PapagoTranslator 에서 ClientId와 Client Secret를 입력해주세요

![image](https://github.com/patcher454/PapagoTranslator-PowerToys/assets/34996184/04e52ac1-c032-479a-8803-0d8073ddbc32)

Alt + Space(기본값) 키를 눌러 PowerToys  Run을 활성화 하고

다음과 같이 입력하시면 번역 결과가 출력됩니다.

결과에 Enter키를 입력하시면 Ctrl + V 키로 붙여넣기가 가능합니다. 

@@{변억 결과 언어 코드} {변역 하고 싶은 문장}

```
@@en 안녕하세요
```
**@@(직접 활성화 명령)은 플러그인 설정에 전역 결과에 포함 옵션을 활성화 하시면 생략 가능합니다.**

## 지원 언어 및 언어코드
1. ko   (Korean)
2. en   (English)
3. ja   (Japanese)
4. cn   (Simplified Chinese)
5. tw   (Traditional Chinese)
6. vi   (Vietnamese)
7. id   (Indonesian)
8. th   (Thai)
9. de   (German)
10. ru  (Russian)
11. es  (Spanish)
12. it  (Italian)
13. fr  (French)

## 설치 방법

[릴리즈 목록](https://github.com/patcher454/PapagoTranslator-PowerToys/releases)

릴리즈 목록에서 최신 버전 .zip 파일을 다운로드 받아주세요.

다음 디렉토리에 다운받은 파일을 풀어주시고 PowerToys를 재시작 해주시면 설치됩니다.

```
C:\Program Files\PowerToys\RunPlugins
```

## 빌드 방법
해당 플러그인 PowerToys 솔루션 안에서 빌드되어야 합니다. 

[PowerToys Repository](https://github.com/microsoft/PowerToys)

```
\src\modules\launcher\Plugins
```

Plugins 디렉토리 아래에 해당 플러그인 프로젝트를 위치시키고 빌드하시면 됩니다.

Visual Studios 2022 이상 버전으로 빌드하시는걸 추천드립니다.
