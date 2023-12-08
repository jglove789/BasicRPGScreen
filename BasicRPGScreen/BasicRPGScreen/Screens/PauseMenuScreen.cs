using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPGScreen.Screens
{
    public class PauseMenuScreen : MenuScreen
    {
        public PauseMenuScreen() : base("Paused")
        {
            var resumeGameMenuEntry = new MenuEntry("Resume Game");
            var characterGameMenuEntry = new MenuEntry("Character Info");
            var inventoryGameMenuEntry = new MenuEntry("Inventory and Equipment");
            var saveGameMenuEntry = new MenuEntry("Save");
            var quitGameMenuEntry = new MenuEntry("Save and Quit Game");

            resumeGameMenuEntry.Selected += OnCancel;
            characterGameMenuEntry.Selected += CharacterGameMenuEntrySelected;
            inventoryGameMenuEntry.Selected += InventoryGameMenuEntrySelected;
            saveGameMenuEntry.Selected += SaveGameMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(characterGameMenuEntry);
            MenuEntries.Add(inventoryGameMenuEntry);
            MenuEntries.Add(saveGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }


        private void CharacterGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();
        }


        private void InventoryGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();
        }


        private void SaveGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Save Game?";
            var saveGameMessageBox = new MessageBoxScreen(message);

            saveGameMessageBox.Accepted += ConfirmedSaveMessageBoxAccepted;

            ScreenManager.AddScreen(saveGameMessageBox, ControllingPlayer);
        }

        private void ConfirmedSaveMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.SaveGame();
        }


        private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit?";
            var confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        // This uses the loading screen to transition from the game back to the main menu screen.
        private void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.SaveGame();
            LoadingScreen.Load(ScreenManager, false, null, new MainMenuScreen());
        }
    }
}
