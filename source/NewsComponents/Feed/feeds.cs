﻿#region CVS Version Header

/*
 * $Id$
 * Last modified by $Author$
 * Last modified at $Date$
 * $Revision$
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

using NewsComponents.Utils;
using RssBandit.AppServices.Core;

namespace NewsComponents.Feed
{  
    /// <remarks/>
    [XmlType(Namespace=NamespaceCore.Feeds_vCurrent)]
    [XmlRoot("feeds", Namespace=NamespaceCore.Feeds_vCurrent, IsNullable=false)]
    public class feeds
    {
        /// <remarks/>
        [XmlElement("feed", Type = typeof (NewsFeed), IsNullable = false)]
        //[XmlElement("feed", Type = typeof(WindowsRssNewsFeed), IsNullable = false)]
        public List<NewsFeed> feed = new List<NewsFeed>();

        /// <remarks/>
        [XmlArrayItem("category", Type = typeof (category), IsNullable = false)]
        public List<category> categories = new List<category>();

		///// <remarks/>
		//[XmlArray("listview-layouts")]
		//[XmlArrayItem("listview-layout", Type = typeof (listviewLayout), IsNullable = false)]
		//public List<listviewLayout> listviewLayouts = new List<listviewLayout>();

        /// <remarks/>
        [XmlArrayItem("server", Type = typeof (NntpServerDefinition), IsNullable = false)]
        [XmlArray(ElementName = "nntp-servers", IsNullable = false)]
        public List<NntpServerDefinition> nntpservers = new List<NntpServerDefinition>();

        /// <remarks/>
        [XmlArrayItem("identity", Type = typeof (UserIdentity), IsNullable = false)]
        [XmlArray(ElementName = "user-identities", IsNullable = false)]
        public List<UserIdentity> identities = new List<UserIdentity>();

        /// <remarks/>
        [XmlAttribute("refresh-rate")]
        public int refreshrate;

        /// <remarks/>
        [XmlIgnore]
        public bool refreshrateSpecified;

        /// <remarks/>
        [XmlAttribute("create-subfolders-for-enclosures"), DefaultValue(false)]
        public bool createsubfoldersforenclosures;

        /// <remarks/>
        [XmlIgnore]
        public bool createsubfoldersforenclosuresSpecified;

        /// <remarks/>
        [XmlAttribute("download-enclosures")]
        public bool downloadenclosures;

        /// <remarks/>
        [XmlIgnore]
        public bool downloadenclosuresSpecified;


        /// <remarks/>
        [XmlAttribute("enclosure-cache-size-in-MB")]
        public int enclosurecachesize;

        /// <remarks/>
        [XmlIgnore]
        public bool enclosurecachesizeSpecified;

        /// <remarks/>
        [XmlAttribute("num-enclosures-to-download-on-new-feed")]
        public int numtodownloadonnewfeed;

        /// <remarks/>
        [XmlIgnore]
        public bool numtodownloadonnewfeedSpecified;


        /// <remarks/>
        [XmlAttribute("enclosure-alert")]
        public bool enclosurealert;

        /// <remarks/>
        [XmlIgnore]
        public bool enclosurealertSpecified;


        /// <remarks/>
        [XmlAttribute("mark-items-read-on-exit")]
        public bool markitemsreadonexit;

        /// <remarks/>
        [XmlIgnore]
        public bool markitemsreadonexitSpecified;

        /// <remarks/>
        [XmlAttribute("enclosure-folder")]
        public string enclosurefolder;


        /// <remarks/>
        [XmlAttribute("podcast-folder")]
        public string podcastfolder;

        /// <remarks/>
        [XmlAttribute("podcast-file-exts")]
        public string podcastfileexts;


        ///<summary>ID to an FeedColumnLayout</summary>
        /// <remarks/>
        [XmlAttribute("listview-layout")]
        public string listviewlayout;

        /// <remarks/>
        [XmlAttribute("max-item-age", DataType="duration")]
        public string maxitemage;

        /// <remarks/>
        [XmlAttribute]
        public string stylesheet;

        ///<summary>ID to an FeedColumnLayout</summary>
        /// <remarks/>
        [XmlAttribute("unread-items-listview-layout")]
        public string unreaditemslistviewlayout;

        /// <remarks/>
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr;
    }

    public interface INewsFeedCategory : ISharedProperty, IEquatable<INewsFeedCategory>
	{ 		
        string Value { get; set; }
        INewsFeedCategory parent { get; set; }
		XmlAttribute[] AnyAttr { get; set; }
    }


    /// <remarks/>
    [XmlType(Namespace=NamespaceCore.Feeds_vCurrent)]
    [DebuggerDisplay("Category = {Value}")]
    public class category : INewsFeedCategory, IEquatable<category>
    {
        /// <summary>
        /// A category must have a name
        /// </summary>
        protected category(){;}

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="name">The name of the category</param>
        public category(string name) {
            if (StringHelper.EmptyTrimOrNull(name))
                throw new ArgumentNullException("name");

            this.Value = name; 
        }

        /// <summary>
        /// Creates a new category by initializing it from an existing one. 
        /// </summary>
        /// <param name="categorytoclone">The category whose properties are being copied</param>
        public category(INewsFeedCategory categorytoclone)
        {
            if (categorytoclone != null)
            {
                this.AnyAttr = categorytoclone.AnyAttr;
                this.downloadenclosures = categorytoclone.downloadenclosures;
                this.downloadenclosuresSpecified = categorytoclone.downloadenclosuresSpecified;
                this.enclosurealert = categorytoclone.enclosurealert;
                this.enclosurealertSpecified = categorytoclone.enclosurealertSpecified;
                this.enclosurefolder = categorytoclone.enclosurefolder;
                this.listviewlayout = categorytoclone.listviewlayout;
                this.markitemsreadonexit = categorytoclone.markitemsreadonexit;
                this.markitemsreadonexitSpecified = categorytoclone.markitemsreadonexitSpecified;
                this.maxitemage = categorytoclone.maxitemage;
                this.refreshrate = categorytoclone.refreshrate;
                this.refreshrateSpecified = categorytoclone.refreshrateSpecified;
                this.stylesheet = categorytoclone.stylesheet;
                this.Value = categorytoclone.Value;
            }
        }

        /// <remarks/>
        [XmlAttribute("mark-items-read-on-exit")]
        public bool markitemsreadonexit { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public bool markitemsreadonexitSpecified { get; set; }

        /// <remarks/>
        [XmlAttribute("download-enclosures")]
        public bool downloadenclosures { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public bool downloadenclosuresSpecified { get; set; }

        /// <remarks/>
        [XmlAttribute("enclosure-folder")]
        public string enclosurefolder { get; set; }

        ///<summary>ID to an FeedColumnLayout</summary>
        /// <remarks/>
        [XmlAttribute("listview-layout")]
        public string listviewlayout { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string stylesheet { get; set; }

        /// <remarks/>
        [XmlAttribute("refresh-rate")]
        public int refreshrate { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public bool refreshrateSpecified { get; set; }

        /// <remarks/>
        [XmlAttribute("max-item-age", DataType="duration")]
        public string maxitemage { get; set; }

        /// <remarks/>
        [XmlText]
        public string Value { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public INewsFeedCategory parent { get; set; }

        /// <remarks/>
        [XmlAttribute("enclosure-alert"), DefaultValue(false)]
        public bool enclosurealert { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public bool enclosurealertSpecified { get; set; }

        /// <remarks/>
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr { get; set; }

        #region Static Methods

        /// <summary>
        /// Helper function that gets the list of ancestor categories for a particular category's hierarchy
        /// </summary>
        /// <param name="key">The category whose ancestor's we are seeking</param>
        /// <returns>The list of ancestor categories in the category hierarchy</returns>
        public static List<string> GetAncestors(string key)
        {

            List<string> list = new List<string>();
            string current = String.Empty;
            string[] s = key.Split(FeedSource.CategorySeparator.ToCharArray());

            if (s.Length != 1)
            {

                for (int i = 0; i < (s.Length - 1); i++)
                {
                    current += (i == 0 ? s[i] : FeedSource.CategorySeparator + s[i]);
                    list.Add(current);
                }

            }

            return list;
        }

        #endregion 

        #region Equality methods

        /// <summary>
        /// Tests to see if two category objects represent the same feed. 
        /// </summary>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (category)) return false;
            return Equals((category) obj);
        }

        /// <summary>
        /// Returns a hashcode for a category object. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        #endregion

        public bool Equals(category obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Value, Value);
        }



        #region IEquatable<INewsFeedCategory> Members

        public bool Equals(INewsFeedCategory other)
        {
            return Equals(other as category);
        }

        #endregion
    }




    /// <remarks/>
    [XmlType(Namespace=NamespaceCore.Feeds_vCurrent)]
    [XmlInclude(typeof(GoogleReaderNewsFeed))]
    [DebuggerDisplay("Title = {title}, Uri = {link}")]
    public class NewsFeed : INewsFeed
    {

        #region constructor

        /// <summary>
        /// Initializes the class.
        /// </summary>
        public NewsFeed() { ; }

        /// <summary>
        /// Initializes the class from an INewsFeed instance
        /// </summary>
        /// <param name="feedtoclone">The feed to obtain it's properties from</param>
         public NewsFeed(INewsFeed feedtoclone)
        {
            if (feedtoclone != null)
            {
                this.link = feedtoclone.link;
                this.title = feedtoclone.title;
                this.category = feedtoclone.category;
                this.cacheurl = cacheurl;
                this.storiesrecentlyviewed = new List<string>(feedtoclone.storiesrecentlyviewed);
                this.deletedstories = new List<string>(feedtoclone.deletedstories);
                this.id = feedtoclone.id;
                this.lastretrieved = feedtoclone.lastretrieved;
                this.lastretrievedSpecified = feedtoclone.lastretrievedSpecified;
                this.lastmodified = feedtoclone.lastmodified;
                this.lastmodifiedSpecified = feedtoclone.lastmodifiedSpecified;
                this.authUser = feedtoclone.authUser;
                this.authPassword = feedtoclone.authPassword;
                this.downloadenclosures = feedtoclone.downloadenclosures;
                this.downloadenclosuresSpecified = feedtoclone.downloadenclosuresSpecified;
                this.enclosurefolder = feedtoclone.enclosurefolder;
                this.replaceitemsonrefresh = feedtoclone.replaceitemsonrefresh;
                this.replaceitemsonrefreshSpecified = feedtoclone.replaceitemsonrefreshSpecified;
                this.refreshrate = feedtoclone.refreshrate;
                this.refreshrateSpecified = feedtoclone.refreshrateSpecified;
                this.maxitemage = feedtoclone.maxitemage;
                this.etag = feedtoclone.etag;
                this.markitemsreadonexit = feedtoclone.markitemsreadonexit;
                this.markitemsreadonexitSpecified = feedtoclone.markitemsreadonexitSpecified;
                this.listviewlayout = feedtoclone.listviewlayout;
                this.favicon = feedtoclone.favicon;
                this.stylesheet = feedtoclone.stylesheet;
                this.enclosurealert = feedtoclone.enclosurealert;
                this.enclosurealertSpecified = feedtoclone.enclosurealertSpecified;
                this.alertEnabled = feedtoclone.alertEnabled;
                this.alertEnabledSpecified = feedtoclone.alertEnabledSpecified;
				
				if (feedtoclone.Any != null)
				{
					this.Any = new XmlElement[feedtoclone.Any.Length];
					feedtoclone.Any.CopyTo(this.Any, 0);
				}
				
				if (feedtoclone.AnyAttr != null)
				{
					this.AnyAttr = new XmlAttribute[feedtoclone.AnyAttr.Length];
					feedtoclone.AnyAttr.CopyTo(this.AnyAttr, 0);
				}
            }
        }

        #endregion 

        #region INewsFeed implementation

        protected string p_title; 
        /// <remarks/>
        public virtual string title {
            get
            {
                return p_title;
            }

            set
            {
                if (String.IsNullOrEmpty(p_title) || !p_title.Equals(value))
                {
                    p_title = value;
                    this.OnPropertyChanged("title"); 
                }
            }
        }

        protected string p_link; 
        /// <remarks/>
        [XmlElement(DataType = "anyURI")]
        public virtual string link
        {
            get
            {
                return p_link;
            }

            set
            {
                if (String.IsNullOrEmpty(p_link) || !p_link.Equals(value))
                {
                    p_link = value;
                    this.OnPropertyChanged("link");
                }
            }
        }

        protected string p_id;
        /// <remarks/>
        [XmlAttribute]
        public virtual string id
        {
            get
            {
                if (string.IsNullOrEmpty(p_id))
                    p_id = Guid.NewGuid().ToString("N");
                return p_id;
            }
            set
            {
                p_id = value;
            }
        }

        protected int p_refreshrate; 
        /// <remarks/>
        [XmlElement("refresh-rate")]
        public virtual int refreshrate
        {
            get
            {
                return p_refreshrate;
            }

            set
            {
                if (!p_refreshrate.Equals(value))
                {
                    p_refreshrate = value;
                    this.OnPropertyChanged("refreshrate");
                }
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool refreshrateSpecified { get; set; }

        /// <remarks/>
        [XmlElement("last-retrieved")]
        public virtual DateTime lastretrieved { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool lastretrievedSpecified { get; set; }

        /// <remarks/>
        public virtual string etag { get; set; }

        /// <remarks/>
        [XmlElement(DataType = "anyURI")]
        public virtual string cacheurl { get; set; }

        protected string p_maxitemage; 
        /// <remarks/>
        [XmlElement("max-item-age", DataType = "duration")]
        public virtual string maxitemage
        {
            get
            {
                return p_maxitemage;
            }

            set
            {
                if (String.IsNullOrEmpty(p_maxitemage) || !p_maxitemage.Equals(value))
                {
                    p_maxitemage = value;
                    this.OnPropertyChanged("maxitemage");
                }
            }
        }

        protected List<string> p_storiesrecentlyviewed = new List<string>();
        /// <remarks/>
        [XmlArray(ElementName = "stories-recently-viewed", IsNullable = false)]
        [XmlArrayItem("story", Type = typeof(String), IsNullable = false)]
        public virtual List<string> storiesrecentlyviewed 
        { 
            get{
                return p_storiesrecentlyviewed;
            }
            set
            {
                p_storiesrecentlyviewed = new List<string>(value);
            }        
        }

        protected List<string> p_deletedstories = new List<string>();
        /// <remarks/>
        [XmlArray(ElementName = "deleted-stories", IsNullable = false)]
        [XmlArrayItem("story", Type = typeof (String), IsNullable = false)]
        public virtual List<string> deletedstories
        {
            get
            {
                return p_deletedstories;
            }
            set
            {
                p_deletedstories = new List<string>(value);
            }
        }


        /// <remarks/>
        [XmlElement("if-modified-since")]
        public virtual DateTime lastmodified { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool lastmodifiedSpecified { get; set; }

		/// <remarks>Client certificate identifier (usually the cert's thumb print value)</remarks>
		[XmlElement("certificate-id")]
		public virtual string certificateId { get; set; }

        /// <remarks/>
        [XmlElement("auth-user")]
        public virtual string authUser { get; set; }

        /// <remarks/>
        [XmlElement("auth-password", DataType = "base64Binary")]
        public virtual Byte[] authPassword { get; set; }

        /// <remarks/>
        [XmlElement("listview-layout")]
        public virtual string listviewlayout { get; set; }

        protected string p_favicon; 
        /// <remarks/>
        public virtual string favicon
        {
            get
            {
                return p_favicon;
            }

            set
            {
                if (String.IsNullOrEmpty(p_favicon) || !p_favicon.Equals(value))
                {
                    p_favicon = value;
                    this.OnPropertyChanged("favicon");
                }
            }
        }


        protected bool p_downloadenclosures; 
        /// <remarks/>
        [XmlElement("download-enclosures")]
        public virtual bool downloadenclosures
        {
            get
            {
                return p_downloadenclosures;
            }

            set
            {
                if (!p_downloadenclosures.Equals(value))
                {
                    p_downloadenclosures = value;
                    this.OnPropertyChanged("downloadenclosures");
                }
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool downloadenclosuresSpecified { get; set; }

        protected string p_enclosurefolder;
        /// <remarks/>
        [XmlElement("enclosure-folder")]
        public virtual string enclosurefolder
        {
            get
            {
                return p_enclosurefolder;
            }

            set
            {
                if (String.IsNullOrEmpty(p_enclosurefolder) || !p_enclosurefolder.Equals(value))
                {
                    p_enclosurefolder = value;
                    this.OnPropertyChanged("enclosurefolder");
                }
            }
        }

        /// <remarks/>
        [XmlAttribute("replace-items-on-refresh")]
        public virtual bool replaceitemsonrefresh { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool replaceitemsonrefreshSpecified {get; set;}

        protected string p_stylesheet; 
        /// <remarks/>
        public virtual string stylesheet
        {
            get
            {
                return p_stylesheet;
            }

            set
            {
                if (String.IsNullOrEmpty(p_stylesheet) || !p_stylesheet.Equals(value))
                {
                    p_stylesheet = value;
                    this.OnPropertyChanged("stylesheet");
                }
            }
        } 

        /// <remarks>Reference the corresponding NntpServerDefinition</remarks>
        [XmlElement("news-account")]
        public virtual string newsaccount { get; set; }

        /// <remarks/>
        [XmlElement("mark-items-read-on-exit")]
        public virtual bool markitemsreadonexit { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool markitemsreadonexitSpecified { get; set; }

        /// <remarks/>
        [XmlAnyElement]
        public XmlElement[] Any { get; set; }


        /// <remarks/>
        [XmlAttribute("alert"), DefaultValue(false)]
        public virtual bool alertEnabled { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool alertEnabledSpecified { get; set; }


        /// <remarks/>
        [XmlAttribute("enclosure-alert"), DefaultValue(false)]
        public virtual bool enclosurealert { get; set; }

        /// <remarks/>
        [XmlIgnore]
        public virtual bool enclosurealertSpecified { get; set; }


        
        /// <summary>
        /// Returns the first element in the categories collection. Setting it replaces all the items in the categories 
        /// collection. 
        /// </summary>
        /// <seealso cref="categories"/>
        [XmlAttribute]
        public virtual string category {
            
            get
            {
            	if (this.categories != null && this.categories.Count > 0)
                {
                    return categories[0]; 
                }
            	return null;
            }

        	set
            {
                this.categories.Clear(); 

                if (!StringHelper.EmptyTrimOrNull(value))
                {
                    this.categories.Add(value); 
                }
            }
        }

        protected List<string> p_categories = new List<string>(); 
        /// <remarks/>
        [XmlArray(ElementName = "categories", IsNullable = false)]
        [XmlArrayItem("category", Type = typeof(String), IsNullable = false)]
        public virtual List<string> categories
        {
            get { return p_categories; }
            set
            {
                if (value != null) { p_categories = value; }
            }
        }

        /// <remarks/>
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr { get; set; }

        /// <remarks>True, if the feed caused an exception on request to prevent sequenced
        /// error reports on every automatic download</remarks>
        [XmlIgnore]
        public virtual bool causedException
        {
            get
            {
                return causedExceptionCount != 0;
            }
            set
            {
                if (value)
                {
                    causedExceptionCount++; // raise counter
                    lastretrievedSpecified = true;
                    lastretrieved = new DateTime(DateTime.Now.Ticks);
                }
                else
                    causedExceptionCount = 0; // reset
            }
        }

        /// <remarks>Number of exceptions caused on requests</remarks>
        [XmlIgnore]
        public virtual int causedExceptionCount { get; set; }

        /// <remarks>Can be used to store any attached data</remarks>
        [XmlIgnore]
        public virtual object Tag { get; set; }

        protected bool p_containsNewMessages;
        /// <remarks/>
        [XmlIgnore]
        public virtual bool containsNewMessages
        {
            get
            {
                return p_containsNewMessages;
            }

            set
            {
                if (!p_containsNewMessages.Equals(value))
                {
                    p_containsNewMessages = value;
                    this.OnPropertyChanged("containsNewMessages");
                }
            }
        }

        protected bool p_containsNewComments;
        /// <remarks/>
        [XmlIgnore]
        public virtual bool containsNewComments
        {
            get
            {
                return p_containsNewComments;
            }

            set
            {
                if (!p_containsNewComments.Equals(value))
                {
                    p_containsNewComments = value;
                    this.OnPropertyChanged("containsNewComments");
                }
            }
        }

        /// <remarks />                
        [XmlIgnore]
        public virtual object owner { get; set; }

		/// <summary>
		/// Gets the value of a particular wildcard element. If the element is not found then
		/// null is returned
		/// </summary>
		/// <param name="f">The f.</param>
		/// <param name="namespaceUri">The namespace URI.</param>
		/// <param name="localName">Name of the local.</param>
		/// <returns>
		/// The value of the wildcard element obtained by calling XmlElement.InnerText
		/// or null if the element is not found.
		/// </returns>
        public static string GetElementWildCardValue(INewsFeed f, string namespaceUri, string localName)
        {
            foreach (XmlElement element in f.Any)
            {
                if (element.LocalName == localName && element.NamespaceURI == namespaceUri)
                    return element.InnerText;
            }
            return null;
        }

        /// <summary>
        /// Removes an entry from the storiesrecentlyviewed collection
        /// </summary>
        /// <seealso cref="storiesrecentlyviewed"/>
        /// <param name="storyid">The ID to add</param>
        public virtual void AddViewedStory(string storyid) {
            if (!p_storiesrecentlyviewed.Contains(storyid)) 
            {
                p_storiesrecentlyviewed.Add(storyid);
                if (null != PropertyChanged)
                {
                    this.OnPropertyChanged(new CollectionChangedEventArgs("storiesrecentlyviewed", CollectionChangeAction.Add, storyid));
                }
            }
        }

        /// <summary>
        /// Adds an entry to the storiesrecentlyviewed collection
        /// </summary>
        /// <seealso cref="storiesrecentlyviewed"/>
        /// <param name="storyid">The ID to remove</param>
        public virtual void RemoveViewedStory(string storyid)
        {
            if (p_storiesrecentlyviewed.Contains(storyid))
            {
                p_storiesrecentlyviewed.Remove(storyid);
                if (null != PropertyChanged)
                {
                    this.OnPropertyChanged(new CollectionChangedEventArgs("storiesrecentlyviewed", CollectionChangeAction.Remove, storyid));
                }
            }
        }

        /// <summary>
        /// Adds a category to the categories collection
        /// </summary>
        /// <seealso cref="categories"/>
        /// <param name="name">The category to add</param>
        public virtual void AddCategory(string name)
        {
            if (!p_categories.Contains(name))
            {
                p_categories.Add(name);
                if (null != PropertyChanged)
                {
                    this.OnPropertyChanged(new CollectionChangedEventArgs("categories", CollectionChangeAction.Add, name));
                }
            }
        }

        /// <summary>
        /// Removes a category from the categories collection
        /// </summary>
        /// <seealso cref="categories"/>
        /// <param name="name">The category to remove</param>
        public virtual void RemoveCategory(string name)
        {
            if (p_categories.Contains(name))
            {
                p_categories.Remove(name);
                if (null != PropertyChanged)
                {
                    this.OnPropertyChanged(new CollectionChangedEventArgs("categories", CollectionChangeAction.Remove, name));
                }
            }
        }

        /// <summary>
        /// Removes an entry from the deletedstories collection
        /// </summary>
        /// <seealso cref="deletedstories"/>
        /// <param name="storyid">The ID to add</param>
        public virtual void AddDeletedStory(string storyid)
        {
            if (!p_deletedstories.Contains(storyid))
            {
                p_deletedstories.Add(storyid);
                if (null != PropertyChanged)
                {
                    this.OnPropertyChanged(new CollectionChangedEventArgs("deletedstories", CollectionChangeAction.Add, storyid));
                }
            }
        }

        /// <summary>
        /// Adds an entry to the deletedstories collection
        /// </summary>
        /// <seealso cref="deletedstories"/>
        /// <param name="storyid">The ID to remove</param>
        public virtual void RemoveDeletedStory(string storyid) {
            if (p_deletedstories.Contains(storyid))
            {
                p_deletedstories.Remove(storyid);
                if (null != PropertyChanged)
                {
                    this.OnPropertyChanged(new CollectionChangedEventArgs("deletedstories", CollectionChangeAction.Remove, storyid));
                }
            }
        
        }

        #endregion 

        #region INotifyPropertyChanged implementation 

        /// <summary>
        ///  Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fired whenever a property is changed. 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(DataBindingHelper.GetPropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies listeners that a property has changed. 
        /// </summary>
        /// <param name="e">Details on the property change event</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion 

        #region public methods
        /// <summary>
        /// Tests to see if two NewsFeed objects represent the same feed. 
        /// </summary>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            NewsFeed feed = obj as NewsFeed;

            if (feed == null)
            {
                return false;
            }

            if (link.Equals(feed.link))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hashcode for a NewsFeed object. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return link.GetHashCode();
        }

        #endregion 
    }

    #region UserIdentity

    /// <remarks/>
    [XmlType(Namespace=NamespaceCore.Feeds_vCurrent)]
    public class UserIdentity : IUserIdentity, ICloneable
    {
        private string name;

        /// <remarks/>
        [XmlAttribute("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string realName;

        /// <remarks/>
        [XmlElement("real-name")]
        public string RealName
        {
            get
            {
                return realName;
            }
            set
            {
                realName = value;
            }
        }

        private string organization;

        /// <remarks/>
        [XmlElement("organization")]
        public string Organization
        {
            get
            {
                return organization;
            }
            set
            {
                organization = value;
            }
        }

        private string mailAddress;

        /// <remarks/>
        [XmlElement("mail-address")]
        public string MailAddress
        {
            get
            {
                return mailAddress;
            }
            set
            {
                mailAddress = value;
            }
        }

        private string responseAddress;

        /// <remarks/>
        [XmlElement("response-address")]
        public string ResponseAddress
        {
            get
            {
                return responseAddress;
            }
            set
            {
                responseAddress = value;
            }
        }

        private string referrerUrl;

        /// <remarks/>
        [XmlElement("referrer-url")]
        public string ReferrerUrl
        {
            get
            {
                return referrerUrl;
            }
            set
            {
                referrerUrl = value;
            }
        }

        private string signature;

        /// <remarks/>
        [XmlElement("signature")]
        public string Signature
        {
            get
            {
                return signature;
            }
            set
            {
                signature = value;
            }
        }

        /// <remarks/>
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr;

        /// <remarks/>
        [XmlAnyElement]
        public XmlElement[] Any;

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }

    #endregion

    #region NewsServerDefinition

    /// <remarks/>
    [XmlType(Namespace=NamespaceCore.Feeds_vCurrent)]
    public class NntpServerDefinition : INntpServerDefinition, ICloneable
    {
        private string name;

        /// <remarks/>
        [XmlAttribute("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string defaultIdentity;

        /// <remarks/>
        [XmlElement("default-identity")]
        public string DefaultIdentity
        {
            get
            {
                return defaultIdentity;
            }
            set
            {
                defaultIdentity = value;
            }
        }

        private bool preventDownloadOnRefresh;

        /// <remarks/>
        [XmlElement("prevent-download")]
        public bool PreventDownloadOnRefresh
        {
            get
            {
                return preventDownloadOnRefresh;
            }
            set
            {
                preventDownloadOnRefresh = value;
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool PreventDownloadOnRefreshSpecified;

        private string server;

        /// <remarks/>
        [XmlElement("server-address")]
        public string Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
            }
        }

        private string authUser;

        /// <remarks/>
        [XmlElement("auth-user")]
        public string AuthUser
        {
            get
            {
                return authUser;
            }
            set
            {
                authUser = value;
            }
        }

        private Byte[] authPassword;

        /// <remarks/>
        [XmlElement("auth-password", DataType="base64Binary")]
        public Byte[] AuthPassword
        {
            get
            {
                return authPassword;
            }
            set
            {
                authPassword = value;
            }
        }

        private bool useSecurePasswordAuthentication;

        /// <remarks/>
        [XmlElement("auth-use-spa")]
        public bool UseSecurePasswordAuthentication
        {
            get
            {
                return useSecurePasswordAuthentication;
            }
            set
            {
                useSecurePasswordAuthentication = value;
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool UseSecurePasswordAuthenticationSpecified;

        private int port;

        /// <remarks/>
        [XmlElement("port-number")]
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool PortSpecified;

        private bool useSSL;

        /// <remarks>Makes the 'nntp:' a 'nntps:'</remarks>
        [XmlElement("use-ssl")]
        public bool UseSSL
        {
            get
            {
                return useSSL;
            }
            set
            {
                useSSL = value;
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool UseSSLSpecified;

        private int timeout;

        /// <remarks/>
        [XmlElement("timeout")]
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool TimeoutSpecified;

        /// <remarks/>
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr;

        /// <remarks/>
        [XmlAnyElement]
        public XmlElement[] Any;

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }

    #endregion
}

#region CVS Version Log

/*
 * $Log: feeds.cs,v $
 * Revision 1.31  2006/12/16 23:15:51  carnage4life
 * Fixed issue where comment feeds get confused when a comment is deleted from the feed,
 *
 * Revision 1.30  2006/12/12 16:20:56  carnage4life
 * Fixed issue where Attachments/Podcasts option "Enable alert window for new downl. attachments" did not get persisted
 *
 * Revision 1.29  2006/12/09 22:57:03  carnage4life
 * Added support for specifying how many podcasts downloaded from new feeds
 *
 * Revision 1.28  2006/11/22 16:29:00  carnage4life
 * Fixed issue where ThreadAbortException throws a user facing error when loading a stylesheet
 *
 * Revision 1.27  2006/11/21 17:25:53  carnage4life
 * Made changes to support options for Podcasts
 *
 * Revision 1.26  2006/11/20 22:26:20  carnage4life
 * Added support for most of the Podcast and Attachment options except for podcast file extensions and copying podcasts to a specified folder
 *
 * Revision 1.25  2006/11/19 03:11:10  carnage4life
 * Added support for persisting podcast settings when changed in the Preferences dialog
 *
 * Revision 1.24  2006/10/28 23:10:00  carnage4life
 * Added "Attachments/Podcasts" to Feed Properties and Category properties dialogs.
 *
 * Revision 1.23  2006/10/05 15:46:29  t_rendelmann
 * rework: now using XmlSerializerCache everywhere to get the XmlSerializer instance
 *
 * Revision 1.22  2006/10/05 08:00:13  t_rendelmann
 * refactored: use string constants for our XML namespaces
 *
 * Revision 1.21  2006/08/18 19:10:57  t_rendelmann
 * added an "id" XML attribute to the NewsFeed. We need it to make the feed items (feeditem.id + feed.id) unique to enable progressive indexing (lucene)
 *
 */

#endregion