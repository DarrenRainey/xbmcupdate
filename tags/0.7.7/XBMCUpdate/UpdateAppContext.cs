/*
 *   XBMCUpdate: Automatic Update Client for XBMC. (www.xbmc.org)
 * 
 *   Copyright (C) 2009  Keivan Beigi
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NLog;
using System.Threading;

namespace XbmcUpdate.Runtime
{

    internal class UpdateAppContext : ApplicationContext
    {
        #region Private Members
        private System.ComponentModel.IContainer mComponents;   //List of components
        private NotifyIcon mNotifyIcon;
        private ContextMenuStrip mContextMenu;
        private ToolStripMenuItem mUpdate;
        private ToolStripMenuItem mDisplayForm;
        private ToolStripMenuItem mExitApplication;
        private UpdateGui frmUpdate;

        #endregion

        private static Logger logger = LogManager.GetCurrentClassLogger();

        internal UpdateAppContext( bool silentUpdate, bool startInTray )
        {
            try
            {
                //Instantiate the component Module to hold everything
                mComponents = new System.ComponentModel.Container();
                frmUpdate = new UpdateGui();
                frmUpdate.FormClosed += new FormClosedEventHandler( frmUpdate_FormClosed );

                if( startInTray )
                {
                    InitTray();
                }
                else
                {
                    frmUpdate.Show();
                }

                if( silentUpdate )
                {
                    frmUpdate.SilentUpdate = true;
                    frmUpdate.StartUpdate();
                }

            }
            catch( Exception e )
            {
                logger.Fatal( "App Context fail. {0}", e.ToString() );
            }



            //mDisplayForm_Click( null, null );
        }

        void frmUpdate_FormClosed( object sender, FormClosedEventArgs e )
        {
            ExitThreadCore();
        }

        private void InitTray()
        {
            //Instantiate the NotifyIcon attaching it to the components container and 
            //provide it an icon, note, you can embed this resource 
            mNotifyIcon = new NotifyIcon( this.mComponents );
            mNotifyIcon.Icon = XbmcUpdate.Runtime.Properties.Resources.app;
            mNotifyIcon.Text = "XBMCUpdate";
            mNotifyIcon.Visible = true;

            //Instantiate the context menu and items
            mContextMenu = new ContextMenuStrip();
            mDisplayForm = new ToolStripMenuItem();
            mExitApplication = new ToolStripMenuItem();
            mUpdate = new ToolStripMenuItem();

            //Attach the menu to the notify icon
            mNotifyIcon.ContextMenuStrip = mContextMenu;


            //Setup the items and add them to the menu strip, adding handlers to be created later

            mUpdate.Text = "Update XBMC";
            mUpdate.Click += new EventHandler( mUpdate_Click );
            mContextMenu.Items.Add( mUpdate );

            mDisplayForm.Text = "Show XBMCUpdate";
            mDisplayForm.Click += new EventHandler( mDisplayForm_Click );
            mContextMenu.Items.Add( mDisplayForm );

            mExitApplication.Text = "Exit";
            mExitApplication.Click += new EventHandler( mExitApplication_Click );
            mContextMenu.Items.Add( mExitApplication );
            mNotifyIcon.DoubleClick += new EventHandler( mNotifyIcon_DoubleClick );
        }
        void mNotifyIcon_DoubleClick( object sender, EventArgs e )
        {
            frmUpdate.Show();
        }

        void mUpdate_Click( object sender, EventArgs e )
        {
            frmUpdate.StartUpdate( );
        }

        void mDisplayForm_Click( object sender, EventArgs e )
        {
            frmUpdate.Show();
        }

        void mExitApplication_Click( object sender, EventArgs e )
        {
            //Call our overridden exit thread core method!
            ExitThreadCore();
        }

        protected override void ExitThreadCore()
        {
            //Clean up any references needed
            //At this time we do not have any
            //

            //Call the base method to exit the application
            if( mNotifyIcon != null )
            {
                mNotifyIcon.Dispose();
            }

            Thread.Sleep( 1000 );

            System.Diagnostics.Process.GetCurrentProcess().Kill();

            //Application.Exit();
        }
    }
}
