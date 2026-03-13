# Image_Slide_Show - 專案與程式應用分析 (AGENTS.md)

這是一個以 C# 與 Windows Forms (WinForms) 開發的桌面版圖片輪播 (Slide Show) 應用程式。以下是該專案的程式結構與主要應用功能分析：

## 1. 專案基本介紹
- **專案名稱**: Image_SlideShow
- **開發語言與框架**: C# (.NET Framework) / WinForms
- **主要用途**: 在 Windows 桌面上提供一個簡單、無邊框、可自訂時間間隔的圖片輪播顯示工具。

## 2. 核心功能 (Core Features)
- **多種圖片格式支援**: 支援 `.jpg`, `.jpeg`, `.gif`, `.bmp`, `.png`, `.tif` 等常見圖片格式。
- **拖放圖片 (Drag & Drop)**: 使用者可將單一圖片檔案或整個資料夾拖曳至應用程式視窗，程式會背景建立圖片播放清單。
- **隨機/循序播放 (Shuffle/Sequential)**: 支援打亂播放順序 (Shuffle Play)，或是依資料夾讀取順序循序播放，並有防重複連續播放的權重演算法。
- **無邊框與全螢幕**: 可以切換無邊框視窗模式，或是全螢幕輪播，且程式視窗具備拖曳與縮放的機制 (`resizeMode` 與自訂滑鼠事件處理)。
- **播放導覽與歷史紀錄 (Navigation History)**: 
  - 支援「上一張/下一張」的操作體驗，無論是隨機或循序模式，皆能透過歷史紀錄 (`historyIndex`) 追蹤最近播放的圖片（最多保留 32 張歷史軌跡）。
  - **沉浸式回憶探索**: 輪播時可隨時無縫切換 **順序 (Sequential)** 與 **隨機 (Shuffle)** 模式。當隨機抽到某張勾起回憶的照片時，可立刻暫停或切換回順序模式，透過方向鍵前後翻閱當時同一個事件點拍下的周遭照片。
  - 優化隨機權重管理 (`randomPickInx`)，避免同樣的照片過早重複被抽到；所有照片皆播放過後會自動重置權重。
- **豐富的快捷鍵與滑鼠操控**:
  - `S`：暫停/繼續播放。
  - `空白鍵`：切換隨機 (Random) / 順序 (Sequential) 播放模式。
  - `滑鼠點擊照片(畫面)`：顯示/隱藏周邊 UI (切換無邊框乾淨展示模式)。
  - `左/右方向鍵` 或 `滑鼠滾輪`：手動切換上一張/下一張。
  - `Esc`：快速退出全螢幕或關閉程式。
  - `F` / `F11`：切換全螢幕。
- **螢幕顯示 (OSD)**: 在熱鍵操作時提供即時的文字回饋效果。
- **組態檔記錄 (config.ini)**: 使用 `.ini` 檔案保存使用者的偏好設定，包含：輪播間隔時間、最上層顯示 (TopMost)、全螢幕、顯示工作列、隨機播放狀態等。
- **支援贊助 (Donate)**: 內建 PayPalDonate 連結，引導至贊助頁面。

## 3. 主要程式碼結構與類別 (Code Structure)

- **`Program.cs`**: 應用程式的進入點，初始化 Visual Styles 並啟動 `Form1` 作為主視窗。
- **`Form1.cs` / `Form1.Designer.cs`**: 主視窗與核心邏輯所在，包含了：
  - 各種 Windows Forms 視窗元件設定 (`PictureBox`, `Timer`, `StatusStrip`, `ContextMenuStrip`)。
  - `fetchFileList`: 背景執行緒負責走訪資料夾中的所有圖檔並建立播放清單。
  - `timerLoop_Tick`, `advanceImage`, `previousImage`: 定時觸發或手動觸發圖片切換邏輯，整合出統一的歷史紀錄 (`historyList`) 導覽設計。
  - `randomPickInx`: 隨機圖片的挑選演算法，並加入隨機權重避免短時間內重複播放同一張圖，且當所有圖片皆顯示後支援權重歸零。
  - `ProcessCmdKey` / `OnMouseWheel`: 處理各種鍵盤快捷鍵語句以及滑鼠滾讀操作。
  - `ShowOsd`: OSD 提示視窗的渲染邏輯。
  - 滑鼠事件處理 (`MouseDown`, `MouseMove`, `MouseUp`): 實作自定義的拖曳視窗以及無邊框視窗的邊緣縮放 (Resize)。
- **`IniRW.cs`**: 封裝了調用 Windows API `WritePrivateProfileString` 與 `GetPrivateProfileString` 的靜態方法，用來讀取與寫入 `config.ini`。
- **`SetDuration.cs` / `SetDuration.Designer.cs`**: 設定輪播間隔時間 (Duration) 的小對話方塊視窗。
- **`AboutSoft.cs` / `AboutSoft.Designer.cs`**: 關於軟體的視窗，顯示軟體版本與相關資訊。
- **`FrmDonate.cs` / `Donate.cs`**: 處理贊助 (Donation) 視窗與 PayPal 按鈕連結功能。

## 4. 特色實作手法 (Technical Highlights)
- **多執行緒防卡頓 (Multi-threading)**: 當大量圖片被拖曳進視窗時，透過開啟新的 `Thread(fetchFileList)` 去處理檔案走訪 (`Directory.GetFiles`)，避免卡住主視窗 UI (並關閉了 `CheckForIllegalCrossThreadCalls = false` 使跨執行緒更新 UI 更加簡化，雖然這不是最佳實作，但是在這類小工具中很常見)。
- **自定義非標題列拖曳**: 因為將視窗改為無邊框樣式，程式在 `Form1_MouseMove` 計算了游標相對於視窗的像素距離，手動實現了包含 8 個方位的邊緣縮放以及主視窗的區域拖曳效果。
- **Ini 檔案外部載入清單 (`.ini` 清單讀取)**: 支援讀取特殊格式的 `.ini` 檔案，當作圖片播放清單的入口。

## 5. 建議與功能擴充 (Suggestions & Enhancements)
這是一個非常實用的基礎輪播工具，目前已經實現部分進階操控，如果希望追求更強大或更符合現代使用者的需求，可以考慮增加以下功能或進行優化：

### 5.1 實用工具與系統整合
- **刪除目前圖片**：增加一個快速鍵（例如 `Delete` 鍵）或右鍵選單，可以直接將目前正在顯示的圖片移至資源回收桶（適合用來快速整理照片）。
- **多螢幕支援 (Multi-Monitor Support)**：如果使用者有多個螢幕，允許指定輪播要顯示在哪一個螢幕上，而不是預設的主螢幕。
- **開機自動啟動與最小化到系統匣 (System Tray)**：適合將舊電腦或螢幕當作電子相框使用時，開機即可自動隱藏在右下角並開始輪播。
- **資料夾監控 (Folder Monitoring)**：使用 `FileSystemWatcher` 監聽匯入的資料夾，當有新圖片加入時，自動更新播放清單。

### 5.2 程式碼架構與現代化建議
- **解決跨執行緒警告 (Cross-Thread Calls)**：目前程式使用 `Control.CheckForIllegalCrossThreadCalls = false` 來繞過多執行緒更新 UI 的限制，這在複雜程式中容易產生不可預期的崩潰。建議改用 `Invoke` 或 `BeginInvoke`，或者升級使用 `async/await` 與 `Task` 來處理背景讀取，會更現代且安全。
- **升級為 WPF 或 WinUI 3**：如果追求更流暢的硬體加速動畫渲染、現代化 UI 與更進階的視覺效果，可考慮將專案移植至 WPF 或 .NET MAUI / WinUI 3 框架。

## 6. 總結
`Image_Slide_Show` 是一個典型、輕量、具有實用性的 WinForms 小工具範例。它包含了本地端檔案處理、系統 API 呼叫 (設定檔與熱鍵)、多執行緒背景讀取、自訂視窗樣式操控、自定歷史重播權重等關鍵實務技巧，適合用作學習 C# 與 Windows UI 互動機制的基礎專案。結合上述的建議擴充，它可以變成一個非常完善的桌面電子相框或展場輪播軟體。
