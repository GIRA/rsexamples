const { mod } = require("./utils");

const RADIANS_PER_DEGREE = Math.PI/180;

function d2r(deg) { return deg*RADIANS_PER_DEGREE; }
function r2d(rad) { return rad/RADIANS_PER_DEGREE; }

function normalize(rad) { return mod(rad, Math.PI*2); }

function radians(rad) {
    return normalize(rad);
}

function degrees(deg) {
    return normalize(d2r(deg));
}

function opposite(rad) {
    return normalize(rad+Math.PI);
}

function distClockwise(a, b) {
    return normalize(a - b);
}

function distCounterclockwise(a, b) {
    return normalize(b - a);
}

function dist(a, b) {
    return Math.min(
        distClockwise(a, b), 
        distCounterclockwise(a, b)
    );
}

module.exports = {
    d2r: d2r,
    r2d: r2d,
    radians: radians,
    degrees: degrees,
    opposite: opposite,
    dist: dist,
    distClockwise: distClockwise,
    distCounterclockwise: distCounterclockwise,
};