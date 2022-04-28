(ns rsteam.math.angle)

(def ^:const PI Math/PI)
(def ^:const HALF_PI (/ PI 2))
(def ^:const TAU (* 2 PI))
(def ^:const RADIANS_PER_DEGREE (/ PI 180))

(defn degrees-to-radians [degrees]
  (* degrees RADIANS_PER_DEGREE))

(defn radians-to-degrees [radians]
  (/ radians RADIANS_PER_DEGREE))

(defn- normalize [radians] (mod radians TAU))

(defn degrees [d]
  (normalize (degrees-to-radians d)))

(defn radians [r]
  (normalize r))

(defn opposite [r]
  (normalize (+ r PI)))

(defn- diff-clockwise [a b] (normalize (- a b)))
(defn- diff-counterclockwise [a b] (normalize (- b a)))

(defn diff
  ([a b]
    (min (diff-clockwise a b)
         (diff-counterclockwise a b)))
  ([a b mode]
    (case mode
      :clockwise (diff-clockwise a b)
      :counterclockwise (diff-counterclockwise a b)
      (throw (ex-info "Invalid mode" {:angles [a b]
                                      :mode mode})))))
