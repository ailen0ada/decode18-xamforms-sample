# decode18-xamforms-sample
Xamarin.Forms を使ったデスクトップアプリケーションの展開例です。
Xamarin.Forms はモバイル開発のためだけのものではありません。データアクセス層をはじめとしたコアとなる処理を共有して，デスクトップアプリケーションを異なるプラットフォームに向けて展開することができるということをこのサンプルで示します。

## 前提条件
次の環境を使って構築します。

- Visual Studio 2017 15.7.1 (Windows 10 1803)
- Visual Studio for Mac 7.5.1.22 (macOS High Sierra 10.13.4)
- Xamarin.Mac 4.4.1.178
- MonoDevelop 7.5.0.1254 (Ubuntu 18.04)
- すべての環境で .NET Core SDK 2.0 以降がインストールされていること

iOS, Androidについてはこのサンプルでは対象としないため，SDKインストール等は不要です。

## サンプルアプリケーション
画像ファイルを入力とし，メディアンカット法による減色を行い，4色カラーパレットを出力するアプリケーションです。次の要素を含みます。

- Xamarin.Forms 3.0 アプリケーションをプロジェクトテンプレートを使わずに構築する
- 共通の処理を .NET Standard 2.0 プロジェクトにまとめる
- 各プラットフォームに処理を委譲する

## プロジェクト構成
- XamForms.DesktopSample
  - XamForms.DesktopSample.Core - UI定義と減色処理
  - XamForms.DesktopSample.Mac - Xamarin.Mac実装
  - XamForms.DesktopSample.Wpf - WPF (Windows Presentation Platform) 実装
  - XamForms.DesktopSample.GtkSharp - Gtk# 実装

## 各プラットフォームで Xamarin.Forms アプリケーションを動かす

### Core
いずれかの　Visual Studio で作業を開始します。執筆時点でスムーズに構築を進めるには Mac を使われることをお勧めします。

#### 新規.NET Standard 2.0ライブラリプロジェクトを作成する
始めに作成されている `Class1.cs` は削除してかまいません。
#### Xamarin.Forms パッケージを導入する
NuGet パッケージを追加します。執筆時点ではバージョン `3.0.0.482510` が最新版でした。
#### App.xaml/App.xaml.cs を作成する
`Forms ContentPage` テンプレートを使って作成し， `Application` として振る舞うように書き換えます。
テンプレートを使わずとも、適当なファイルを作って `App.xaml`, `App.xaml.cs` として同じような内容にしておいてもかまいません。
#### MainPage.xaml/MainPage.xaml.cs を作成する
`Forms ContentPage` テンプレートを使って作成します。これも同様に適当に作っても構いません。テンプレートを使って作るとプロジェクトファイルに `DependentUpon` が作られ、ファイルの関係性がIDEで見やすくなる程度のことです。XAMLの中身は現段階では適当にしておきます。おおよそ次のようになるでしょう。

```MainPage.xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="XamForms.DesktopSample.Core.MainPage">
    <ContentPage.Content>
        <StackLayout Orientation="Vertical" VerticalOptions="CenterAndExpand" HorizontalOptions="CenterAndExpand">
            <Label Text="Welcome to Xamarin Forms!" VerticalOptions="Center" HorizontalOptions="Center" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

### macOS
#### 新規Cocoa Appプロジェクトを作成する
#### Info.plistを編集
`NSMainStoryboardFile` のエントリを削除します。起動時に表示されるユーザーインタフェースをこちらでコントロールできるようになります。

#### Main.storyboard, ViewController.cs を削除
UIはCoreプロジェクトで作成しているので不要です。

#### Xamarin.Forms パッケージを導入する
Coreと同様に導入します。また，忘れずにCoreプロジェクトへの参照を追加します。

#### AppDelegate.cs を編集
`FormsApplicationDelegate` として振る舞うように書き換えます。

### WPF
WPF については Windows の Visual Studio を使います。

#### 新規WPF Appプロジェクトを作成する

#### Xamarin.Forms, Xamarin.Forms.Platform.Wpf パッケージを導入する
Xamarin.Formsと、WPF向けパッケージを別にインストールします。また，忘れずにCoreプロジェクトへの参照を追加します。

#### MainWindow.xaml を編集する
`Window` タグに次の記述を追加し、名前空間を参照できるようにします。

```
xmlns:wpf="clr-namespace:Xamarin.Forms.Platform.WPF;assembly=Xamarin.Forms.Platform.WPF"
```

`Window` タグを `wpf:FormsApplicationPage` に変更します。
XAML側を変更したのでコードビハインド側も変更が必要になります。`MainWindow.xaml.cs` を開いて基底クラスを変更したうえで、初期化コードを記述します。

### Gtk#
Gtk# についてはWindowsでもMacでもLinuxでも作業を行うことができます。ここではUbuntuを利用します。

#### 新規 Gtk# 2.0 アプリケーションプロジェクトを作成する
VisualStudio for Mac，および MonoDevelop には該当のプロジェクトテンプレートがあります。

#### MainWindow.cs を削除する
特に使いません。

#### Xamarin.Forms, Xamarin.Forms.Platform.GTK パッケージを導入する
Xamarin.Formsと、Gtk#向けパッケージを別にインストールします。また，忘れずにCoreプロジェクトへの参照を追加します。

#### Program.cs を書き換えて Core.App を呼び出す
特に他にやることはありません。

### ここまでのまとめ

1. Coreプロジェクトを作ってUIとAppクラスを作る
2. それぞれのプラットフォームに合わせたプロジェクトを作る
3. 各プラットフォームごとにXamarin.Formsパッケージと必要に応じてPlatformパッケージを導入する
4. Core.App を各プラットフォームの流儀で呼び出す

今後どんな Xamarin.Forms のサポート対象プラットフォームが増えたとしてもやることはほぼ同じです。そして VisualStudio で Xamarin.Forms プロジェクトテンプレートを使って一式作ったものも，基本的な部分は全く同じことをやっています。まずはここまで（Welcome to Xamarin.Forms!が表示されるまで）を各プラットフォームで作ってみて，その構成を眺めてみてください。

## プラットフォームに処理を委譲する
各プラットフォームでUIを表示することはできるようになったので，もう少し複雑なUIを定義した上で，インタラクションを各プラットフォームに委譲してみましょう。具体的には，ボタンクリックでファイルを選択させるインタラクションを定義します。

### インタラクション
次のようなインタラクションを考えます。

1. 選択ボタンをクリックする
2. ファイル選択ダイアログで画像ファイルを選ぶ
3. 選んだ画像ファイルのパスと画像がUIに表示される

CoreプロジェクトのXAMLを編集してこれを可能にするUIを定義します。ここでは画像表示コントロール，色表示のためのビュー，ラベル，ボタンを配置します。

### ファイル選択機能の注入
ファイル選択は各プラットフォームそれぞれのお作法で面倒を見てやる必要があります。次のようなインタフェースを考え，この実装を各プラットフォームで行うこととします。

```
interface IImageFileSelector
{
    FileInfo PickImageFile();
}
```

Dependency Injection (DI) のような仕組みを使う， Xamarin Plugin を使うといった方法は考えられますが，本サンプルの範囲を超えるので素直に Application クラスをインスタンス化するときに注入してしまうことにします。

```
public partial class App : Application
{
    public IImageFileSelector ImageFileSelector { get; }

    public App(IImageFileSelector selector)
    {
        InitializeComponent();

        MainPage = new MainPage();
        ImageFileSelector = selector;
    }
}
```

`MainPage` で選択ボタンがクリックされたときの処理は次のようになるでしょう。本サンプルではコードビハインドから直接アクセスします。

```
void SelectImageFile(object sender, System.EventArgs e)
{
    var selector = (App.Current as Core.App)?.ImageFileSelector;
    if (selector == null) return;

    var file = selector.PickImageFile();
    if (file == null || !file.Exists) return;

    this.PathLabel.Text = file.FullName;
    this.ImageView.Source = ImageSource.FromFile(file.FullName);
}
```

まずはここまでで，各プラットフォームでファイル選択を実装しましょう。

#### macOS
`NSOpenPanel` を使ってしまえばいいでしょう。

```
public class ImageFileSelector : IImageFileSelector
{
    public FileInfo PickImageFile()
    {
        var ofd = new NSOpenPanel
        {
            AllowedFileTypes = new[] { "png", "jpg" }
        };
        var ret = ofd.RunModal();
        return ret < 1 ? null : new FileInfo(ofd.Url.Path);
    }
}
```

`AppDelegate` で `Core.App` をインスタンス化しているところで，こいつもインスタンス化してやって引数に渡します。

#### WPF

#### Gtk#
`FileChooserDialog` を使います。

```
public FileInfo PickImageFile()
{
    var filter = new FileFilter { Name = "Image files" };
    filter.AddPattern("*.png");
    filter.AddPattern("*.jpg");
    var fcd = new FileChooserDialog("Choose image file", null, FileChooserAction.Open, "Cancel", ResponseType.Close, "Select", ResponseType.Accept)
    {
        SelectMultiple = false,
        Filter = filter
    };
    try
    {
        return fcd.Run() == (int)ResponseType.Accept ? new FileInfo(fcd.Filename) : null;
    }
    finally
    {
        fcd.Destroy();
    }
}
```

`Program.cs` でコンストラクタの引数に渡すのを忘れずに。