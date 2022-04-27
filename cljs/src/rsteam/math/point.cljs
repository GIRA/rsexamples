(ns rsteam.math.point
  (:require [rsteam.math.angle :as a]
            [rsteam.math.utils :as m]))

(def ^:const ORIGIN {:x 0 :y 0})

(defn point [& {:keys [x y] :or {x 0 y 0}}]
  {:x x :y y})

(defn vec->point [[x y]]
  {:x x :y y})

(defn angle->point
  ([angle] (angle->point angle 1))
  ([angle magnitude]
   (point :x (* -1 magnitude (Math/sin angle))
          :y (* magnitude (Math/cos angle)))))

(defn dist [{x :x y :y} {x' :x y' :y}]
  (let [dx (- x' x)
        dy (- y' y)]
    (Math/sqrt (+ (* dx dx) (* dy dy)))))

(defn magnitude [p]
  (dist ORIGIN p))

(defn angle [{:keys [x y]}]
  (if (and (zero? x)
           (zero? y))
    (a/radians 0)
    (a/radians (Math/atan2 (* x -1) y))))

(defn average [points]
  (when (seq points)
    (let [x (/ (reduce + (map :x points))
               (count points))
          y (/ (reduce + (map :y points))
               (count points))]
      (point :x x :y y))))

(defn keep-inside-rectangle
  [{:keys [x y] :as point} {:keys [origin corner]}]
  (assoc point
         :x (m/clamp x (:x origin) (:x corner))
         :y (m/clamp y (:y origin) (:y corner))))