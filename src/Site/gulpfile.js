/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
  fs = require("fs"),
  rimraf = require("rimraf"),
  concat = require("gulp-concat"),
  cssmin = require("gulp-cssmin"),
  uglify = require("gulp-uglify"),
  sass = require("gulp-sass"),
  project = require("./project.json");

var paths = {
  webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.sass = paths.webroot + "css/**/*.scss";
paths.sqliteDb = './releases-db.sqlite';

gulp.task("clean:js", function(cb) {
  rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function(cb) {
  rimraf(paths.concatCssDest, cb);
});

gulp.task("clean:sqlite", function(cb) {
  fs.open(paths.sqliteDb, 'w', cb);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:sqlite"]);

gulp.task("clean-node-sass-src", function (cb) {
    // There's a *.vcxproj file in this directory. As of ASP.NET 5 beta 6, DNU will attempt (and fail) to compile it,
    // so we need to make sure it's gone before it gets that far.
    rimraf("./node_modules/gulp-sass/node_modules/node-sass/src/libsass/win/", cb);
});

gulp.task('sass', function () {
  return gulp.src(paths.sass, {base: paths.webroot + "css/scss"})
    .pipe(sass())
    .pipe(gulp.dest(paths.webroot + "css/"));
});

gulp.task("min:js", function() {
  gulp.src([paths.js, "!" + paths.minJs], {
      base: "."
    })
    .pipe(concat(paths.concatJsDest))
    .pipe(uglify())
    .pipe(gulp.dest("."));
});

gulp.task('watch', function() {
    gulp.watch(paths.sass, ['sass']);
});

gulp.task("min:css", function() {
  gulp.src([paths.css, "!" + paths.minCss])
    .pipe(concat(paths.concatCssDest))
    .pipe(cssmin())
    .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css", "sass"]);
