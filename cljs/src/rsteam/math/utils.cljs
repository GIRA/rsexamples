(ns rsteam.math.utils)

(defn close?
  ([a b] (close? a b (.-EPSILON js/Number)))
  ([a b tolerance] (< (Math/abs (- a b)) tolerance)))

(defn clamp [n min' max']
  (max min' (min max' n)))

(defn sign [n]
  (if (pos? n) 1
      (if (neg? n) -1 0)))