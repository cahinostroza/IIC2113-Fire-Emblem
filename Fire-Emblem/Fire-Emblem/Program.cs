// See https://aka.ms/new-console-template for more information

using Fire_Emblem;
using Fire_Emblem_View;

var view = View.BuildConsoleView();
var game = new Game(view, "data/E1-BasicCombat");
game.Play();
