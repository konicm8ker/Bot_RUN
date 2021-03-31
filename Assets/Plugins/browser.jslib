mergeInto(LibraryManager.library, {

  GetBrowser: function () {
    var isFirefox = typeof InstallTrigger !== 'undefined';
    return isFirefox;
  }

});
