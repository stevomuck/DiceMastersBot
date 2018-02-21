using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Net.Providers.WS4Net;
using Discord.Commands;

namespace DiceBot
{
    class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        //private DependencyMap map;

        static void Main(string[] args)
        {
            new Program().MyBotStart().GetAwaiter().GetResult();
        }

        public async Task MyBotStart()
        {
            //Declaring variables.
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                WebSocketProvider = WS4NetProvider.Instance
            });
            //map = new DependencyMap();
            client.Log += Log;
            string token = "MzM3MjgxMjA5NDYzODY1MzQ0.DFXxww.3u3MmawAW6W2w4Et6aHzMLFTywE";
            commands = new CommandService();

            //Initialize command handling.
            await InstallCommands();

            //Connect bot to Discord.
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            await client.SetGameAsync(">help for more info");

            //Prevents bot from dying naturally to end of code.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            System.Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task InstallCommands()
        {
            client.MessageReceived += CommandHandler;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task CommandHandler(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;

            if (message == null)
            {
                return;
            }

            int argPos = 1;

            if (!(message.HasStringPrefix(">", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)))
            {
                return;
            }

            var context = new CommandContext(client, message);

            //var result = await commands.ExecuteAsync(context, argPos, map);
            var result = await commands.ExecuteAsync(context, argPos);

            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }


    }
}
