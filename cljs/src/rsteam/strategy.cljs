(ns rsteam.strategy
  (:require [rsteam.robot :as r]
            [rsteam.math.angle :as a]
            [rsteam.math.point :as p]))

(def ball-follower {:name ::ball-follower})
(def goalkeeper {:name ::goalkeeper})

(defmulti choose-action (fn [robot _snapshot] (-> robot :role :name)))

(defmethod choose-action :default [_ _] nil)

(defmethod choose-action ::goalkeeper [robot {:keys [ball]}]
  (let [target (p/point :y -0.55 
                        :x (:x ball))]
    (if (< (p/dist robot target) 0.01)
      (r/look-at-angle robot (a/degrees 90))
      (r/move-to-point robot target))))

(defmethod choose-action ::ball-follower [robot {:keys [ball]}]
  (r/move-to-point robot ball))

(defn update-role [snapshot robot-idx]
  (update-in snapshot [:robots robot-idx]
             assoc :role
             (if (zero? robot-idx)
               goalkeeper
               ball-follower)))

(defn update-action [snapshot robot-idx]
  (update-in snapshot [:robots robot-idx]
             (fn [robot] (assoc robot :wheels 
                                (choose-action robot snapshot)))))

(defn run [snapshot robot-idx]
  (-> snapshot
      (update-role robot-idx)
      (update-action robot-idx)))
