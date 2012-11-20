<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Data" %>
[
    {
        "key": 1,
        "name": "Folder1",
        "iconCls": "icon-ok"
    },
    {
        "key": 2,
		"pid": 1,
        "name": "File1",
        "checked": true
    },
    {
        "key": 3,
		"pid": 1,
        "name": "Folder2",
        "state": "open"
    },
    {
        "key": 4,
        "pid": 3,
        "name": "File3",
        "attributes": {
            "p1": "value1",
            "p2": "value2"
        },
        "checked": true,
        "iconCls": "icon-reload"
    },
    {
        "key": 8,
        "pid": 3,
        "name": "Async Folder"
    },
    {
        "key": 9,
        "name": "language",
        "state": "closed"
    },
    {
        "key": "j1",
        "pid": 9,
        "name": "Java"
    },
    {
        "key": "j2",
        "pid": 9,
        "name": "C#"
    }
]