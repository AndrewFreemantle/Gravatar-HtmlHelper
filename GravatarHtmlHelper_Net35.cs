using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Reflection;



/// <summary>
/// Globally Recognised Avatar - http://gravatar.com
/// </summary>
/// <remarks>
///
/// For .Net 3.5 / ASP.Net MVC 2.0
///
/// This implementation by Andrew Freemantle - http://www.fatlemon.co.uk/
/// <para>Source, Wiki and Issues: https://github.com/AndrewFreemantle/Gravatar-HtmlHelper </para>
/// </remarks>
public static class GravatarHtmlHelper {

    /// <summary>
    /// In addition to allowing you to use your own image, Gravatar has a number of built in options which you can also use as defaults. Most of these work by taking the requested email hash and using it to generate a themed image that is unique to that email address
    /// </summary>
    public enum DefaultImage {
        /// <summary>Default Gravatar logo</summary>
        [DescriptionAttribute("")]
        Default,
        /// <summary>404 - do not load any image if none is associated with the email hash, instead return an HTTP 404 (File Not Found) response</summary>
        [DescriptionAttribute("404")]
        Http404,
        /// <summary>Mystery-Man - a simple, cartoon-style silhouetted outline of a person (does not vary by email hash)</summary>
        [DescriptionAttribute("mm")]
        MysteryMan,
        /// <summary>Identicon - a geometric pattern based on an email hash</summary>
        [DescriptionAttribute("identicon")]
        Identicon,
        /// <summary>MonsterId - a generated 'monster' with different colors, faces, etc</summary>
        [DescriptionAttribute("monsterid")]
        MonsterId,
        /// <summary>Wavatar - generated faces with differing features and backgrounds</summary>
        [DescriptionAttribute("wavatar")]
        Wavatar,
        /// <summary>Retro - awesome generated, 8-bit arcade-style pixelated faces</summary>
        [DescriptionAttribute("retro")]
        Retro
    }


    /// <summary>
    /// Gravatar allows users to self-rate their images so that they can indicate if an image is appropriate for a certain audience. By default, only 'G' rated images are displayed unless you indicate that you would like to see higher ratings
    /// </summary>
    public enum Rating {
        /// <summary>Suitable for display on all websites with any audience type</summary>
        [DescriptionAttribute("g")]
        G,
        /// <summary>May contain rude gestures, provocatively dressed individuals, the lesser swear words, or mild violence</summary>
        [DescriptionAttribute("pg")]
        PG,
        /// <summary>May contain such things as harsh profanity, intense violence, nudity, or hard drug use</summary>
        [DescriptionAttribute("r")]
        R,
        /// <summary>May contain hardcore sexual imagery or extremely disturbing violence</summary>
        [DescriptionAttribute("x")]
        X
    }


    /// <summary>
    /// Returns a Globally Recognised Avatar as an 80 pixel &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress) {
        return GravatarImage(htmlHelper, emailAddress, 80, DefaultImage.Default, string.Empty, false, Rating.G, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, string.Empty, false, Rating.G, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="cssClass">CSS class attribute (default: "gravatar")</param>
    /// <param name="alt">Image alt attribute (default: "Gravatar image")</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string cssClass, string alt) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, string.Empty, false, Rating.G, false, cssClass, alt);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImage">Default image if user hasn't created a Gravatar</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, DefaultImage defaultImage) {
        return GravatarImage(htmlHelper, emailAddress, size, defaultImage, string.Empty, false, Rating.G, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, false, Rating.G, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImage">Default image if user hasn't created a Gravatar</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, DefaultImage defaultImage, bool forceDefaultImage) {
        return GravatarImage(htmlHelper, emailAddress, size, defaultImage, string.Empty, forceDefaultImage, Rating.G, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl, bool forceDefaultImage) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, forceDefaultImage, Rating.G, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImage">Default image if user hasn't created a Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, DefaultImage defaultImage, Rating rating) {
        return GravatarImage(htmlHelper, emailAddress, size, defaultImage, string.Empty, false, rating, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl, Rating rating) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, false, rating, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImage">Default image if user hasn't created a Gravatar</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, DefaultImage defaultImage, bool forceDefaultImage, Rating rating) {
        return GravatarImage(htmlHelper, emailAddress, size, defaultImage, string.Empty, forceDefaultImage, rating, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl, bool forceDefaultImage, Rating rating) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, forceDefaultImage, rating, false, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImage">Default image if user hasn't created a Gravatar</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    /// <param name="forceSecureRequest">Always do secure (https) requests</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, DefaultImage defaultImage, bool forceDefaultImage, Rating rating, bool forceSecureRequest) {
        return GravatarImage(htmlHelper, emailAddress, size, defaultImage, string.Empty, forceDefaultImage, rating, forceSecureRequest, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    /// <param name="forceSecureRequest">Always do secure (https) requests</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl, bool forceDefaultImage, Rating rating, bool forceSecureRequest) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, forceDefaultImage, rating, forceSecureRequest, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    /// <param name="forceSecureRequest">Always do secure (https) requests</param>
    /// <param name="cssClass">CSS class attribute (default: "gravatar")</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl, bool forceDefaultImage, Rating rating, bool forceSecureRequest, string cssClass)
    {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, forceDefaultImage, rating, forceSecureRequest, string.Empty, string.Empty);
    }

    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    /// <param name="emailAddress">Email Address for the Gravatar</param>
    /// <param name="size">Size in pixels (default: 80)</param>
    /// <param name="defaultImageUrl">URL to a custom default image (e.g: 'Url.Content("~/images/no-grvatar.png")' )</param>
    /// <param name="forceDefaultImage">Prefer the default image over the users own Gravatar</param>
    /// <param name="rating">Gravatar content rating (note that Gravatars are self-rated)</param>
    /// <param name="forceSecureRequest">Always do secure (https) requests</param>
    /// <param name="cssClass">CSS class attribute (default: "gravatar")</param>
    /// <param name="alt">Image alt attribute (default: "Gravatar image")</param>
    public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, string defaultImageUrl, bool forceDefaultImage, Rating rating, bool forceSecureRequest, string cssClass, string alt) {
        return GravatarImage(htmlHelper, emailAddress, size, DefaultImage.Default, defaultImageUrl, forceDefaultImage, rating, forceSecureRequest, cssClass, alt);
    }




    /// <summary>
    /// Returns a Globally Recognised Avatar as an &lt;img /&gt; - http://gravatar.com
    /// </summary>
    private static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, int size, DefaultImage defaultImage, string defaultImageUrl, bool forceDefaultImage, Rating rating, bool forceSecureRequest, string cssClass, string alt) {
        var imgTag = new TagBuilder("img");

        emailAddress = String.IsNullOrEmpty(emailAddress) ? String.Empty : emailAddress.Trim().ToLower();

        imgTag.Attributes.Add("src",
            string.Format("{0}://{1}.gravatar.com/avatar/{2}?s={3}{4}{5}{6}",
                htmlHelper.ViewContext.HttpContext.Request.IsSecureConnection || forceSecureRequest ? "https" : "http",
                htmlHelper.ViewContext.HttpContext.Request.IsSecureConnection || forceSecureRequest ? "secure" : "www",
                GetMd5Hash(emailAddress),
                size.ToString(),
                "&d=" + (!string.IsNullOrEmpty(defaultImageUrl) ? HttpUtility.UrlEncode(defaultImageUrl) : defaultImage.GetDescription()),
                forceDefaultImage ? "&f=y" : "",
                "&r=" + rating.GetDescription()
                )
            );

        cssClass = String.IsNullOrEmpty(cssClass) ? "gravatar" : cssClass;
        alt = String.IsNullOrEmpty(alt) ? "Gravatar image" : alt;

        imgTag.Attributes.Add("class", cssClass);
        imgTag.Attributes.Add("alt", alt);
        return new HtmlString(imgTag.ToString(TagRenderMode.SelfClosing));
    }


    /// <summary>
    /// Generates an MD5 hash of the given string
    /// </summary>
    /// <remarks>Source: http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx </remarks>
    private static string GetMd5Hash(string input) {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++) {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }


    /// <summary>
    /// Returns the value of a DescriptionAttribute for a given Enum value
    /// </summary>
    /// <remarks>Source: http://blogs.msdn.com/b/abhinaba/archive/2005/10/21/483337.aspx </remarks>
    /// <param name="en"></param>
    /// <returns></returns>
    private static string GetDescription(this Enum en) {

        Type type = en.GetType();
        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0) {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),false);

            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }

        return en.ToString();

    }

}
