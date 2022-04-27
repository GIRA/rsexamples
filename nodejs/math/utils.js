
function mod(n, d) {
    return ((n % d) + d) % d;
}

function clamp(n, min, max) {
    return Math.max(min, Math.min(max, n));
}

module.exports = {
    mod: mod,
    clamp: clamp
};