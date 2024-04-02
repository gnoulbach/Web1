using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Support
{
    public class Support
    {
        public static string InTrang(int total, int sotrang, int size)
        {
            int tongsotrang = (int)(total / size);
            if (tongsotrang * size < total)
            {
                tongsotrang++;
            }

            var html = "<style>" +
                           ".pagination-container { " +
                               "display: flex; " +
                               "list-style: none; " +
                               "padding: 0; " +
                               "margin: 20px auto; " + // Center the pagination by setting margin auto
                           "} " +
                           ".pagination-link { " +
                               "margin: 0 5px; " +
                               "padding: 5px 10px; " +
                               "text-decoration: none; " +
                               "border: 1px solid #ccc; " +
                               "border-radius: 5px; " +
                               "color: #333; " +
                           "} " +
                           ".pagination-link:hover { " +
                               "background-color: #ddd; " +
                           "} " +
                       "</style>" +
                       "<ul class='pagination-container'>";

            if (tongsotrang > 5)
            {
                if (tongsotrang - sotrang >= 3)
                {
                    if (sotrang - 1 > 0)
                    {
                        html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + (sotrang - 1) + ")'>" + (sotrang - 1) + "</a></li>";
                    }

                    html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + sotrang + ")'>" + sotrang + "</a></li>";
                    html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + (sotrang + 1) + ")'>" + (sotrang + 1) + "</a></li>";
                    html += "<li>...</li>";
                    html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + tongsotrang + ")'>" + tongsotrang + "</a></li>";
                }
                else
                {
                    if (tongsotrang - sotrang == 0)
                    {
                        html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + 1 + ")'>" + 1 + "</a></li>";
                        html += "<li>...</li>";
                        for (int i = tongsotrang - 2; i <= tongsotrang; i++)
                        {
                            html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + i + ")'>" + i + "</a></li>";
                        }
                    }
                    if (tongsotrang - sotrang > 0)
                    {
                        for (int i = sotrang - 1; i <= tongsotrang; i++)
                        {
                            html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + i + ")'>" + i + "</a></li>";
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= tongsotrang; i++)
                {
                    if (sotrang == i)
                    {
                        html += "<li><a class='pagination-link' href='javascript:void(0)'>" + i + "</a></li>";
                    }
                    else
                    {
                        html += "<li><a class='pagination-link' href='javascript:void(0)' onclick='goto(" + i + ")'>" + i + "</a></li>";
                    }
                }
            }

            html += "</ul>";
            return html;
        }
    }
}