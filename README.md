#Gravatar HtmlHelper for ASP.Net MVC

This is a complete implementation of the [Gravatar](http://www.gravatar.com) image request API, according the [Gravatar documentation](http://en.gravatar.com/site/implement/images/), as an ASP.Net MVC HtmlHelper.


##Features

* Size
* Default image (404, mystery-man, identicon, monsterid, wavatar, retro)
* Custom default image (url)
* Force default image
* Ratings
* Secure requests (HTTPS) is automatic (but can be forced)
* Named and Optional Arguments for .Net 4.0 and above


##Getting Started

1. Drop the `GravatarHtmlHelper_{version}.cs` file anywhere in your ASP.Net MVC project:

	`GravatarHtmlHelper_NetCore.cs`  :new:  
	_for **.Net Core / ASP.Net Core MVC**_

	`GravatarHtmlHelper_Net40.cs`  
	_for **.Net 4.0+ / ASP.Net MVC 3, 4, 5+**_

	`GravatarHtmlHelper_Net35.cs`  
	_for **.Net 3.5 / ASP.Net MVC 2.0**_


2. Recompile and start using it in your Views:

    `@Html.GravatarImage("user.name@email.com")`

    `@Html.GravatarImage("user.name@email.com", size: 32, defaultImage: GravatarHtmlHelper.DefaultImage.Identicon, rating: GravatarHtmlHelper.Rating.PG)`


That's it. No attribution required, but feel free to [leave a comment on my blog](http://www.fatlemon.co.uk/gravatar) if you find it useful


## Alternatives
Mark Mailer has a [TagHelper implementation](https://github.com/ia2o/Gravatar-TagHelper) for ASP.Net Core MVC