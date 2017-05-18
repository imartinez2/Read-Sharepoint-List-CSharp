 //﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;


namespace BrowserAPP
{

  public struct typBanners
  {
    public string Msg;
    public DateTime From;
    public DateTime To;
    public bool Enabled;
  }


  public struct typButtons
  {
    public string Title;
    public string URL;
  }


  class SharepointHelpper
  {
    public static ListItemCollection GetList(string listName)
    {
      var tenant = BrowserAPP.Properties.Settings.Default.SPSite; //Must set the sharepoint site url
      var userName = BrowserAPP.Properties.Settings.Default.SPUser; //username
      var passwordString = BrowserAPP.Properties.Settings.Default.SPPass; //password

      using (var ctx = new ClientContext(tenant))
      {
        //Provide count and pwd for connecting to the source
        var passWord = new SecureString();
        foreach (var c in passwordString.ToCharArray()) passWord.AppendChar(c);
        ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);

        // Actual code for operations
        var web = ctx.Web;
        ctx.Load(web);
        ctx.ExecuteQuery();

        //Get my list
        var query = new CamlQuery();
        var myList = web.Lists.GetByTitle(listName);
        var kundeItems = myList.GetItems(query);

        ctx.Load<List>(myList);
        ctx.Load<ListItemCollection>(kundeItems);
        ctx.ExecuteQuery();
        return kundeItems;
      }

    }
  }
}
